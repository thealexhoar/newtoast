use std::{ffi::{CStr, CString, OsString}, mem::transmute, os::raw::{c_char, c_void}};

use widestring::U16CString;

use crate::{dotnet::{DotnetContext, DotnetFunction, DotnetFunctionPtr}, render::RenderServer, runtime_interface::config::{InitConfig, RawInitConfig}, util::parse_hstr_wide};


#[repr(C)]
struct EntrypointSpecifier {
    asm_name: *const u16,
    class_name: *const u16,
    is_set: bool,
}

impl EntrypointSpecifier {
    fn to_strings<F: Fn(*const u16) -> ()>(self, free_hstr: F) -> (U16CString, U16CString) {
        let asm = parse_hstr_wide(self.asm_name).unwrap();
        let class = parse_hstr_wide(self.class_name).unwrap();
        free_hstr(self.asm_name);
        free_hstr(self.class_name);
        (asm, class)
    }
}

type GetConfigFn = fn() -> RawInitConfig;
type InitializeFn = fn();
type ShutdownFn = fn();
type UpdateFn = fn(f64);
type DrawFn = fn();
type FreeHstrFn = fn(*const i16);

type BindSingletonFn = fn(*const c_void) -> ();
type UnbindSingletonFn = fn() -> ();

pub struct RuntimeEntrypoints {
    get_config_fptr: DotnetFunctionPtr,
    initialize_fptr: DotnetFunctionPtr,
    shutdown_fptr: DotnetFunctionPtr,
    update_fptr: DotnetFunctionPtr,
    draw_fptr: DotnetFunctionPtr,

    free_hstr_fptr: DotnetFunctionPtr,

    bind_render_server: DotnetFunctionPtr,
    unbind_render_server: DotnetFunctionPtr,
}

impl RuntimeEntrypoints {
    pub fn find_entrypoint_and_build(
        dotnet: &mut DotnetContext
    ) -> Result<Self, &'static str> {
        let internal_entrypoint_class = "NTF.NTEntrypointInternal, NT";

        let get_config_fptr: DotnetFunctionPtr = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "SetupAndGetConfig",
            "NTF.GetConfigFn, NT");
        let initialize_fptr: DotnetFunctionPtr = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "Initialize",
            "NTF.InitializeFn, NT");
        let shutdown_fptr = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "Shutdown",
            "NTF.ShutdownFn, NT");
        let update_fptr = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "Update",
            "NTF.UpdateFn, NT");
        let draw_fptr = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "Draw",
            "NTF.DrawFn, NT");

        let free_hstr_fptr = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "FreeHstr",
            "NTF.FreeHstrFn, NT");

        let bind_render_server = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "BindRenderServer",
            "NTF.BindRenderServerFn, NT");
        let unbind_render_server = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "UnbindRenderServer",
            "NTF.UnbindRenderServerFn, NT");

        Ok(Self {
            get_config_fptr,
            initialize_fptr,
            shutdown_fptr,
            update_fptr,
            draw_fptr,
            free_hstr_fptr,
            bind_render_server,
            unbind_render_server,
        })
    }


    pub fn setup_and_get_config(&self) -> InitConfig {
        unsafe {
            let config_raw = self.get_config_fptr.call::<GetConfigFn>(());
            config_raw.cook(|ptr| {
                self.free_hstr_fptr.call::<FreeHstrFn>((ptr));
            })
        }
    }

    pub fn initialize<'a>(&self) {
        unsafe {
            self.initialize_fptr.call::<InitializeFn>(())
        }
    }

    pub fn shutdown(&self) {
        unsafe {
            self.shutdown_fptr.call::<ShutdownFn>(());
        }
    }

    pub fn update(&self, delta_time: f64) {
        unsafe {
            self.update_fptr.call::<UpdateFn>((delta_time));
        }
    }

    pub fn draw(&self) {
        unsafe {
            self.draw_fptr.call::<DrawFn>(());
        }
    }

    pub fn bind_render_server(&self, server: &mut RenderServer) {
        unsafe {
            self.bind_render_server.call::<BindSingletonFn>(transmute(server));
        }
    }

    pub fn unbind_render_server(&self) {
        unsafe {
            self.unbind_render_server.call::<UnbindSingletonFn>(());
        }
    }

}


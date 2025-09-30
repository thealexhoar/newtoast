use std::{ffi::{CStr, CString, OsString}, mem::transmute, os::raw::{c_char, c_void}};

use widestring::U16CString;

use crate::{dotnet::{DotnetContext, DotnetFunction, DotnetFunctionPtr}, render::RenderServer, runtime_interface::config::RawInitConfig, util::parse_hstr_wide};


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

type InitializeFn = fn();
type ShutdownFn = fn();
type UpdateFn = fn(f64);
type DrawFn = fn();

type BindSingletonFn = fn(*const c_void) -> ();
type UnbindSingletonFn = fn() -> ();

pub struct RuntimeEntrypoints {
    initialize_fptr: DotnetFunctionPtr,
    shutdown_fptr: DotnetFunctionPtr,
    update_fptr: DotnetFunctionPtr,
    draw_fptr: DotnetFunctionPtr,

    bind_render_server: DotnetFunctionPtr,
    unbind_render_server: DotnetFunctionPtr,
}

impl RuntimeEntrypoints {
    pub fn find_entrypoint_and_build(
        dotnet: &mut DotnetContext
    ) -> Result<Self, &'static str> {
        let internal_entrypoint_class = "NTF.NTEntrypointInternal, NT";

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
        let bind_render_server = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "BindRenderServer",
            "NTF.BindRenderServerFn, NT");
        let unbind_render_server = dotnet.get_fn_pointer(
            internal_entrypoint_class,
            "UnbindRenderServer",
            "NTF.UnbindRenderServerFn, NT");

        Ok(Self {
            initialize_fptr,
            shutdown_fptr,
            update_fptr,
            draw_fptr,
            bind_render_server,
            unbind_render_server,
        })
    }


    pub fn initialize<'a>(&self, foo: &u8) {
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


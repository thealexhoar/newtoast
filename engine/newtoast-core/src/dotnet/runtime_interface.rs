use std::{ffi::{CStr, CString, OsString}, os::raw::c_char};

use widestring::U16CString;

use crate::{dotnet::{DotnetContext, DotnetFunction, DotnetFunctionPtr}, util::parse_hstr_wide};


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

type FindEntrypointFn = fn() -> EntrypointSpecifier;
type InitializeFn = fn();
type ShutdownFn = fn();
type UpdateFn = fn(f64);
type DrawFn = fn();

pub struct RuntimeInterface {
    initialize_fptr: DotnetFunctionPtr,
    shutdown_fptr: DotnetFunctionPtr,
    update_fptr: DotnetFunctionPtr,
    draw_fptr: DotnetFunctionPtr,
}

impl RuntimeInterface {
    pub fn find_entrypoint_and_build(
        dotnet: &mut DotnetContext
    ) -> Result<Self, &'static str> {
        let find_entrypoint_fn: DotnetFunctionPtr = dotnet.get_fn_pointer(
            "NTF.NTEntryPoint, NT",
            "IdentifyEntrypoint",
            "NTF.FindEntrypointFn, NT");

        let free_hstr_fn: DotnetFunctionPtr = dotnet.get_fn_pointer(
            "NTF.NTEntryPoint, NT",
            "FreeHstr",
            "NTF.FreeHstrFn, NT"
        );

        unsafe {
            let entrypoint_specifier = find_entrypoint_fn.call::<FindEntrypointFn>(());

            if false == entrypoint_specifier.is_set {
                return Err("Failed to find entrypoint in runtime");
            }
            let (assembly, class) = entrypoint_specifier.to_strings(|hstr_ptr| {
                free_hstr_fn.call::<fn(*const u16)>(hstr_ptr);
            });
            let aq_class = format!("{}, {}", class.to_string_lossy(), assembly.to_string_lossy());
            println!("Found entrypoint class:\n  \"{}\"", &aq_class);


            let initialize_fptr: DotnetFunctionPtr = dotnet.get_fn_pointer(
                &aq_class,
                "Initialize",
                "NTF.InitializeFn, NT");
            let shutdown_fptr = dotnet.get_fn_pointer(
                &aq_class,
                "Shutdown",
                "NTF.ShutdownFn, NT");
            let update_fptr = dotnet.get_fn_pointer(
                &aq_class,
                "Update",
                "NTF.UpdateFn, NT");
            let draw_fptr = dotnet.get_fn_pointer(
                &aq_class,
                "Draw",
                "NTF.DrawFn, NT");
            Ok(Self {
                initialize_fptr,
                shutdown_fptr,
                update_fptr,
                draw_fptr
            })
        }
    }


    pub fn initialize(&self) {
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
}


// src/dotnet/dotnet.rs
// This file is intentionally left blank. Add your Rust code here for dotnet interop or related logic.

use std::mem::transmute;
use std::{ffi::OsString, os::windows::ffi::OsStrExt};
use libloading::{Library, Symbol};

use crate::dotnet::hostfxr::HostfxrDelegate;
use crate::ffi_char_t::char_t;

use crate::dotnet::{hostfxr, nethost};

#[derive(Copy, Clone, Debug)]
pub struct DotnetFunctionPtr {
    fptr: *mut std::ffi::c_void,
}

//HACK the odds this still meets Send/Sync requirements is probably about 6%
// unsafe impl Send for DotnetFunctionPtr {}
// unsafe impl Sync for DotnetFunctionPtr {}

impl DotnetFunctionPtr {
    pub unsafe fn reify<F: DotnetFunction>(&self) -> &F {
        &*(self.fptr as *const F)
    }

    pub unsafe fn call<F: DotnetFunction>(&self, args: F::Args) -> F::Output
    {
        self.reify::<F>().call_dotnet(args)
    }
}


pub unsafe trait DotnetFunction : Sized + Copy + 'static {
    type Args;
    type Output;
    type Managed: ManagedDotnetFunction<Args=Self::Args, Output=Self::Output>;

    unsafe fn call_dotnet(&self, args: Self::Args) -> Self::Output;
}

pub unsafe trait ManagedDotnetFunction : DotnetFunction {
    type Args;
    type Output;
}

macro_rules! dotnet_function_impls {
    (@count ()) => {
        0
    };

    (@count ($first: tt $($rest: tt)*)) => {
        1 + dotnet_function_impls!(@count ($($rest)*))
    };

    (@inner $($arg_name:ident : $arg_type:ident),*) => {
        dotnet_function_impls!(@inner_inner ($($arg_name : $arg_type),*) (fn($($arg_type),*) -> O) (extern "system" fn($($arg_type),*) -> O));
        dotnet_function_impls!(@inner_inner ($($arg_name : $arg_type),*) (unsafe fn($($arg_type),*) -> O) (unsafe extern "system" fn($($arg_type),*) -> O));
    };

    (@inner_inner ($($arg_name:ident : $arg_type : ident),*) ($fn_type:ty) ($ext_type:ty)) => {
        unsafe impl<O: 'static, $($arg_type: 'static),*> DotnetFunction for $fn_type {
            type Args = ($($arg_type),*);
            type Output = O;
            type Managed = $ext_type;

            unsafe fn call_dotnet(&self, args: Self::Args) -> Self::Output {
                assert!(self as *const _ != std::ptr::null());
                let reified_fptr: $ext_type = std::mem::transmute(self);
                let ($($arg_name),*) = args;
                reified_fptr($($arg_name),*)
            }
        }

        unsafe impl<O: 'static, $($arg_type: 'static),*> DotnetFunction for $ext_type {
            type Args = ($($arg_type),*);
            type Output = O;
            type Managed = $ext_type;

            unsafe fn call_dotnet(&self, args: Self::Args) -> Self::Output {
                assert!(self as *const _ != std::ptr::null());
                let reified_fptr: $ext_type = std::mem::transmute(self);
                let ($($arg_name),*) = args;
                reified_fptr($($arg_name),*)
            }
        }

        unsafe impl<O: 'static, $($arg_type: 'static),*> ManagedDotnetFunction for $ext_type {
            type Args = ($($arg_type),*);
            type Output = O;
        }
    };

    (@outer) => {
        dotnet_function_impls!(@inner );
    };

    (@outer $name_first:ident : $type_first:ident $(, $name_rest:ident : $type_rest:ident)*) => {
        dotnet_function_impls!(@inner $name_first : $type_first $(, $name_rest : $type_rest)*);

        dotnet_function_impls!(@outer $($name_rest : $type_rest),*);
    };

    ($($arg_name:ident : $arg_type:ident),*) => {
        dotnet_function_impls!(@outer $($arg_name : $arg_type),*);
    }
}


dotnet_function_impls! {
    __arg1: A1, __arg2: A2, __arg3: A3, __arg4: A4, __arg5: A5, __arg6: A6,
    __arg7: A7, __arg8: A8, __arg9: A9, __arg10: A10, __arg11: A11, __arg12: A12
}


pub struct DotnetContext<'lib> {
    hostfxr_handle: hostfxr::hostfxr_handle,

    hostfxr_lib: &'lib Library,
    init_for_cmd_line: Symbol<'lib, hostfxr::hostfxr_initialize_for_dotnet_command_line_fn>,
    init_with_config: Symbol<'lib, hostfxr::hostfxr_initialize_for_runtime_config_fn>,
    get_delegate: Symbol<'lib, hostfxr::hostfxr_get_runtime_delegate_fn>,
    run_app: Symbol<'lib, hostfxr::hostfxr_run_app_fn>,
    close: Symbol<'lib, hostfxr::hostfxr_close_fn>,

    delegate_get_fptr: hostfxr::get_function_pointer_fn,
    delegate_load_assembly: hostfxr::load_assembly_fn,
    delegate_load_assembly_bytes: hostfxr::load_assembly_bytes_fn,
}

impl<'lib> DotnetContext<'lib> {
    pub fn load_assembly(&mut self, path: &str) {
        unsafe {
            let load_assembly = self.delegate_load_assembly;
            println!("Loading assembly from path:\n  {}", path);
            let path_wide: Vec<u16> = OsString::from(path).encode_wide().chain(std::iter::once('\0' as u16)).collect();
            let rc = load_assembly(
                path_wide.as_ptr() as *const char_t,
                std::ptr::null_mut(),
                std::ptr::null_mut(),
            );
            assert_eq!(rc, 0);
        }
    }

    pub fn get_fn_pointer<'dn>(
        &'dn self,
        type_name: &str,
        method_name: &str,
        delegate_type_name: &str
    ) -> DotnetFunctionPtr {
        unsafe {
            let mut fptr = std::ptr::null_mut();
            let get_fptr = self.delegate_get_fptr;
            let type_name_wide: Vec<u16> = OsString::from(type_name).encode_wide().chain(std::iter::once('\0' as u16)).collect();
            let method_name_wide: Vec<u16> = OsString::from(method_name).encode_wide().chain(std::iter::once('\0' as u16)).collect();
            let delegate_type_name_wide: Vec<u16> = OsString::from(delegate_type_name).encode_wide().chain(std::iter::once('\0' as u16)).collect();
            get_fptr(
                type_name_wide.as_ptr() as *const char_t,
                method_name_wide.as_ptr() as *const char_t,
                delegate_type_name_wide.as_ptr() as *const char_t,
                std::ptr::null_mut(),
                std::ptr::null_mut(),
                &mut fptr
            );
            assert!(!fptr.is_null());

            DotnetFunctionPtr {
                fptr,
            }
        }
    }
}

pub fn load_hostfxr() -> Library {
    let mut buffer: [char_t; 1024] = [0; 1024];
    let mut buffer_size = buffer.len();
    unsafe {
        let rc = nethost::get_hostfxr_path(
            buffer.as_mut_ptr() as *mut char_t,
            &mut buffer_size,
            std::ptr::null(),
        );
        let mut path=  String::from_utf16(&buffer).unwrap();
        path.truncate(buffer_size);
        println!("hostfxr path:\n  {}", path);

        let lib = libloading::Library::new(&path).unwrap();

        lib
    }
}


pub fn create_context<'lib>(hostfxr_lib: &'lib Library, runtimeconfig_path: &str) -> DotnetContext<'lib> {
    unsafe {
        // TODO handle rc
        let init_for_cmd_line: libloading::Symbol<hostfxr::hostfxr_initialize_for_dotnet_command_line_fn>
            = hostfxr_lib.get(b"hostfxr_initialize_for_dotnet_command_line").unwrap();
        let init_with_config: libloading::Symbol<hostfxr::hostfxr_initialize_for_runtime_config_fn>
            = hostfxr_lib.get(b"hostfxr_initialize_for_runtime_config").unwrap();
        let get_delegate: libloading::Symbol<hostfxr::hostfxr_get_runtime_delegate_fn>
            = hostfxr_lib.get(b"hostfxr_get_runtime_delegate").unwrap();
        let run_app: libloading::Symbol<hostfxr::hostfxr_run_app_fn>
            = hostfxr_lib.get(b"hostfxr_run_app").unwrap();
        let close: libloading::Symbol<hostfxr::hostfxr_close_fn>
            = hostfxr_lib.get(b"hostfxr_close").unwrap();

        let mut hostfxr_handle = std::ptr::null_mut();
        let config_path: Vec<u16> = OsString::from(runtimeconfig_path).encode_wide().chain(std::iter::once('\0' as u16)).collect();
        let rc = init_with_config(
            config_path.as_ptr() as *const char_t,
            std::ptr::null_mut(),
            &mut hostfxr_handle
        );
        assert_eq!(rc, 0);

        //TODO error handling

        let mut get_fptr_out: *mut std::ffi::c_void = std::ptr::null_mut();
        let get_fptr_rc = get_delegate(
            hostfxr_handle,
            HostfxrDelegate::GetFunctionPointer as i32,
            &mut get_fptr_out,
        );
        assert_eq!(get_fptr_rc, 0);
        let delegate_get_fptr = std::mem::transmute(get_fptr_out);

        let mut load_assembly_out: *mut std::ffi::c_void = std::ptr::null_mut();
        let load_assembly_rc = get_delegate(
            hostfxr_handle,
            HostfxrDelegate::LoadAssembly as i32,
            &mut load_assembly_out
        );
        assert_eq!(load_assembly_rc, 0);
        let delegate_load_assembly = std::mem::transmute(load_assembly_out);

        let mut load_assembly_bytes_out: *mut std::ffi::c_void = std::ptr::null_mut();
        let load_assembly_bytes_rc = get_delegate(
            hostfxr_handle,
            HostfxrDelegate::LoadAssemblyBytes as i32,
            &mut load_assembly_bytes_out
        );
        assert_eq!(load_assembly_bytes_rc, 0);
        let delegate_load_assembly_bytes = std::mem::transmute(load_assembly_bytes_out);


        DotnetContext {
            hostfxr_handle,
            hostfxr_lib,

            init_for_cmd_line,
            init_with_config,
            get_delegate,
            run_app,
            close,

            delegate_get_fptr,
            delegate_load_assembly,
            delegate_load_assembly_bytes,
        }
    }
}

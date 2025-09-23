use std::os::raw::c_int;
use crate::ffi_char_t::char_t;


// Parameters for get_hostfxr_path
//
// Fields:
//   size
//     Size of the struct. This is used for versioning.
//
//   assembly_path
//     Path to the component's assembly.
//     If specified, hostfxr is located as if the assembly_path is the apphost
//
//   dotnet_root
//     Path to directory containing the dotnet executable.
//     If specified, hostfxr is located as if an application is started using
//     'dotnet app.dll', which means it will be searched for under the dotnet_root
//     path and the assembly_path is ignored.
//
#[repr(C)]
pub struct GetHostfxrParameters {
    pub size: usize,
    pub assembly_path: *const char_t,
    pub dotnet_root: *const char_t,
}

#[cfg_attr(windows, link(name = "nethost"))]
extern "C" {
    /// Get the path to the hostfxr library
    ///
    /// Returns 0 on success, otherwise failure
    pub fn get_hostfxr_path(
        buffer: *mut char_t,
        buffer_size: *mut usize,
        parameters: *const GetHostfxrParameters,
    ) -> c_int;
}

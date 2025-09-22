
use std::os::raw::{c_int, c_void};

#[cfg(windows)]
pub type char_t = u16;
#[cfg(not(windows))]
pub type char_t = u8;

pub enum HostfxrDelegate {
	ComActivation = 0,
	LoadInMemoryAssembly,
	WinrtActivation,
	ComRegistar,
	ComUnregister,
    LoadAssemblyAndGetFunctionPointer,
    GetFunctionPointer,
    LoadAssembly,
    LoadAssemblyBytes,
}

pub type hostfxr_handle = *mut c_void;

#[repr(C)]
pub struct HostfxrInitParameters {
	pub size: usize,
	pub host_path: *const char_t,
	pub dotnet_root: *const char_t,
}

pub type hostfxr_main_fn = unsafe extern "C" fn(argc: c_int, argv: *const *const char_t) -> c_int;

pub type hostfxr_main_startupinfo_fn = unsafe extern "C" fn(
	argc: c_int,
	argv: *const *const char_t,
	host_path: *const char_t,
	dotnet_root: *const char_t,
	app_path: *const char_t,
) -> c_int;

pub type hostfxr_main_bundle_startupinfo_fn = unsafe extern "C" fn(
	argc: c_int,
	argv: *const *const char_t,
	host_path: *const char_t,
	dotnet_root: *const char_t,
	app_path: *const char_t,
	bundle_header_offset: i64,
) -> c_int;

pub type hostfxr_error_writer_fn = unsafe extern "C" fn(message: *const char_t);

pub type hostfxr_set_error_writer_fn = unsafe extern "C" fn(error_writer: hostfxr_error_writer_fn) -> hostfxr_error_writer_fn;

pub type hostfxr_initialize_for_dotnet_command_line_fn = unsafe extern "C" fn(
	argc: c_int,
	argv: *const *const char_t,
	parameters: *const HostfxrInitParameters,
	host_context_handle: *mut hostfxr_handle,
) -> c_int;

pub type hostfxr_initialize_for_runtime_config_fn = unsafe extern "C" fn(
	runtime_config_path: *const char_t,
	parameters: *const HostfxrInitParameters,
	host_context_handle: *mut hostfxr_handle,
) -> c_int;

pub type hostfxr_get_runtime_property_value_fn = unsafe extern "C" fn(
	host_context_handle: hostfxr_handle,
	name: *const char_t,
	value: *mut *const char_t,
) -> c_int;

pub type hostfxr_set_runtime_property_value_fn = unsafe extern "C" fn(
	host_context_handle: hostfxr_handle,
	name: *const char_t,
	value: *const char_t,
) -> c_int;

pub type hostfxr_get_runtime_properties_fn = unsafe extern "C" fn(
	host_context_handle: hostfxr_handle,
	count: *mut usize,
	keys: *mut *const char_t,
	values: *mut *const char_t,
) -> c_int;

pub type hostfxr_run_app_fn = unsafe extern "C" fn(host_context_handle: hostfxr_handle) -> c_int;

pub type hostfxr_get_runtime_delegate_fn = unsafe extern "C" fn(
	host_context_handle: hostfxr_handle,
	r#type: c_int,
	delegate: *mut *mut c_void,
) -> c_int;

pub type hostfxr_close_fn = unsafe extern "C" fn(host_context_handle: hostfxr_handle) -> c_int;

#[repr(C)]
pub struct HostfxrDotnetEnvironmentSdkInfo {
	pub size: usize,
	pub version: *const char_t,
	pub path: *const char_t,
}

#[repr(C)]
pub struct HostfxrDotnetEnvironmentFrameworkInfo {
	pub size: usize,
	pub name: *const char_t,
	pub version: *const char_t,
	pub path: *const char_t,
}

#[repr(C)]
pub struct HostfxrDotnetEnvironmentInfo {
	pub size: usize,
	pub hostfxr_version: *const char_t,
	pub hostfxr_commit_hash: *const char_t,
	pub sdk_count: usize,
	pub sdks: *const HostfxrDotnetEnvironmentSdkInfo,
	pub framework_count: usize,
	pub frameworks: *const HostfxrDotnetEnvironmentFrameworkInfo,
}

pub type hostfxr_get_dotnet_environment_info_result_fn = unsafe extern "C" fn(
	info: *const HostfxrDotnetEnvironmentInfo,
	result_context: *mut c_void,
);

//
// Returns available SDKs and frameworks.
//
// Resolves the existing SDKs and frameworks from a dotnet root directory (if
// any), or the global default location. If multi-level lookup is enabled and
// the dotnet root location is different than the global location, the SDKs and
// frameworks will be enumerated from both locations.
//
// The SDKs are sorted in ascending order by version, multi-level lookup
// locations are put before private ones.
//
// The frameworks are sorted in ascending order by name followed by version,
// multi-level lookup locations are put before private ones.
//
// Parameters:
//    dotnet_root
//      The path to a directory containing a dotnet executable.
//
//    reserved
//      Reserved for future parameters.
//
//    result
//      Callback invoke to return the list of SDKs and frameworks.
//      Structs and their elements are valid for the duration of the call.
//
//    result_context
//      Additional context passed to the result callback.
//
// Return value:
//   0 on success, otherwise failure.
//
// String encoding:
//   Windows     - UTF-16 (pal::char_t is 2 byte wchar_t)
//   Non-Windows - UTF-8  (pal::char_t is 1 byte char)
//
pub type hostfxr_get_dotnet_environment_info_fn = unsafe extern "C" fn(
	dotnet_root: *const char_t,
	reserved: *mut c_void,
	result: hostfxr_get_dotnet_environment_info_result_fn,
	result_context: *mut c_void,
) -> c_int;

#[repr(C)]
pub struct HostfxrFrameworkResult {
	pub size: usize,
	pub name: *const char_t,
	pub requested_version: *const char_t,
	pub resolved_version: *const char_t,
	pub resolved_path: *const char_t,
}

#[repr(C)]
pub struct HostfxrResolveFrameworksResult {
	pub size: usize,
	pub resolved_count: usize,
	pub resolved_frameworks: *const HostfxrFrameworkResult,
	pub unresolved_count: usize,
	pub unresolved_frameworks: *const HostfxrFrameworkResult,
}

pub type hostfxr_resolve_frameworks_result_fn = unsafe extern "C" fn(
	result: *const HostfxrResolveFrameworksResult,
	result_context: *mut c_void,
) -> ();

//
// Resolves frameworks for a runtime config
//
// Parameters:
//    runtime_config_path
//      Path to the .runtimeconfig.json file
//    parameters
//      Optional. Additional parameters for initialization.
//      If null or dotnet_root is null, the root corresponding to the running hostfx is used.
//    callback
//      Optional. Result callback invoked with result of the resolution.
//      Structs and their elements are valid for the duration of the call.
//    result_context
//      Optional. Additional context passed to the result callback.
//
// Return value:
//   0 on success, otherwise failure.
//
// String encoding:
//   Windows     - UTF-16 (pal::char_t is 2-byte wchar_t)
//   Non-Windows - UTF-8  (pal::char_t is 1-byte char)
//
pub type hostfxr_resolve_frameworks_for_runtime_config_fn = unsafe extern "C" fn(
	runtime_config_path: *const char_t,
	parameters: *const HostfxrInitParameters, //optional
	callback: hostfxr_resolve_frameworks_result_fn, //optional
	result_context: *mut c_void, //optional
) -> c_int;



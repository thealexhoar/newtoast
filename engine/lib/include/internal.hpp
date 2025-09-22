#ifndef HAVE_INTERNAL_HPP
#define HAVE_INTERNAL_HPP

#include <assert.h>
#include <windows.h>
#include <stdio.h>

#include "dotnet/hostfxr.h"
#include "dotnet/nethost.h"
#include "dotnet/coreclr_delegates.h"

// platform-specific. commented out is linux option
#define DLLEXPORT __declspec(dllexport)
//#define DLLEXPORT __attribute__ ((__visibility__ ("default")))


struct HostfxrRuntimeDelegates {
    void* com_activation;
    void* load_in_memory_assembly;
    void* winrt_activation;
    void* com_register;
    void* com_unregister;
    load_assembly_and_get_function_pointer_fn load_assembly_and_get_function_pointer;
    get_function_pointer_fn get_function_pointer;
    load_assembly_fn load_assembly;
    load_assembly_bytes_fn load_assembly_bytes;
};

struct HostfxrInterface {
    hostfxr_initialize_for_dotnet_command_line_fn init_for_cmd_line_fptr;
    hostfxr_initialize_for_runtime_config_fn init_for_config_fptr;
    hostfxr_get_runtime_delegate_fn get_delegate_fptr;
    hostfxr_run_app_fn run_app_fptr;
    hostfxr_close_fn close_fptr;
    HostfxrRuntimeDelegates delegates;
};

bool hostfxr_load_lib(HostfxrInterface& iface);

int hostfxr_init_context(HostfxrInterface& iface, hostfxr_handle* outctx);

int hostfxr_populate_delegates(hostfxr_handle ctx, HostfxrInterface& iface);

int dotnet_load_assembly(HostfxrInterface& iface, const char_t* path);

int dotnet_get_function_pointer(
    HostfxrInterface& iface,
    const char_t* type_name,
    const char_t* method_name,
    const char_t* delegate_type_name,
    void** fptr
);

#endif
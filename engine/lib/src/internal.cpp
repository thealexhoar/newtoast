#include "internal.hpp"

// NOTE load_library and get_export platform-specific to windows
// from https://github.com/dotnet/samples/blob/main/core/hosting/src/NativeHost/nativehost.cpp
void* load_library(const char_t* path)
{
    HMODULE h = ::LoadLibraryW(path);
    // assert(h != nullptr);
    return (void*)h;
}

void* get_export(void* h, const char* name) {
    void* f = ::GetProcAddress((HMODULE)h, name);
    assert(f != nullptr);
    return f;
}

bool hostfxr_load_lib(HostfxrInterface& iface) {
    char_t buffer[256];
    size_t buffer_size = sizeof(buffer) / sizeof(char_t);

    int rc = get_hostfxr_path(buffer, &buffer_size, nullptr);
    if (rc != 0) {
        return false;
    }

    printf("hostfxr path: \n  %ls\n", (char_t*)buffer);

    void *lib = load_library(buffer);
    iface.init_for_cmd_line_fptr = (hostfxr_initialize_for_dotnet_command_line_fn)get_export(lib, "hostfxr_initialize_for_dotnet_command_line");
    iface.init_for_config_fptr = (hostfxr_initialize_for_runtime_config_fn)get_export(lib, "hostfxr_initialize_for_runtime_config");
    iface.get_delegate_fptr = (hostfxr_get_runtime_delegate_fn)get_export(lib, "hostfxr_get_runtime_delegate");
    iface.run_app_fptr = (hostfxr_run_app_fn)get_export(lib, "hostfxr_run_app");
    iface.close_fptr = (hostfxr_close_fn)get_export(lib, "hostfxr_close");

    auto ptr = "Loaded";
    auto nptr = "NULL";
    printf("Loading hostfxr fns...\n");
    printf("   hostfxr_initialize_for_dotnet_command_line: %s\n", iface.init_for_cmd_line_fptr ? ptr : nptr);
    printf("   hostfxr_initialize_for_runtime_config:      %s\n", iface.init_for_config_fptr ? ptr : nptr);
    printf("   hostfxr_get_runtime_delegate:               %s\n", iface.get_delegate_fptr ? ptr : nptr);
    printf("   hostfxr_run_app:                            %s\n", iface.run_app_fptr ? ptr : nptr);
    printf("   hostfxr_close:                              %s\n", iface.close_fptr ? ptr : nptr);


    // FIXME copied, revisit
    return (
        iface.init_for_cmd_line_fptr &&
        iface.init_for_config_fptr &&
        iface.get_delegate_fptr &&
        iface.run_app_fptr &&
        iface.close_fptr
    );
}

int hostfxr_init_context(HostfxrInterface& iface, hostfxr_handle* out_ctx) {

    hostfxr_handle ctx;
    int rc = iface.init_for_config_fptr(L"runtimeconfig.json", nullptr, &ctx);
    if (rc != 0 || ctx == nullptr) {
        // failed to init
        printf("Failed to initialize hostfxr via init_for_config_fptr. Code %d\n", rc);
        printf("ctx is null: %s\n", ctx == nullptr ? "true" : "false");

        iface.close_fptr(ctx);
        return rc;
    }

    *out_ctx = ctx;
    return 0;
}

//TODO add logging
int hostfxr_populate_delegates(hostfxr_handle ctx, HostfxrInterface& iface) {
    int com_activation_rc = iface.get_delegate_fptr(
        ctx,
        hdt_com_activation,
        &iface.delegates.com_activation
    );

    int load_in_memory_assembly_rc = iface.get_delegate_fptr(
        ctx,
        hdt_load_in_memory_assembly,
        &iface.delegates.load_in_memory_assembly
    );

    int winrt_activation_rc = iface.get_delegate_fptr(
        ctx,
        hdt_winrt_activation,
        &iface.delegates.winrt_activation
    );

    int com_register_rc = iface.get_delegate_fptr(
        ctx,
        hdt_com_register,
        &iface.delegates.com_register
    );

    int com_unregister_rc = iface.get_delegate_fptr(
        ctx,
        hdt_com_unregister,
        &iface.delegates.com_unregister
    );

    int load_assembly_and_get_fptr_rc = iface.get_delegate_fptr(
        ctx,
        hdt_load_assembly_and_get_function_pointer,
        (void**)&iface.delegates.load_assembly_and_get_function_pointer
    );

    int get_function_pointer_rc = iface.get_delegate_fptr(
        ctx,
        hdt_get_function_pointer,
        (void**)&iface.delegates.get_function_pointer
    );
    assert(get_function_pointer_rc == 0);

    int load_assembly_rc = iface.get_delegate_fptr(
        ctx,
        hdt_load_assembly,
        (void**)&iface.delegates.load_assembly
    );
    assert(load_assembly_rc == 0);

    int load_assembly_bytes_rc = iface.get_delegate_fptr(
        ctx,
        hdt_load_assembly_bytes,
        (void**)&iface.delegates.load_assembly_bytes
    );
    assert(load_assembly_bytes_rc == 0);

    return 0;
}

int dotnet_load_assembly(HostfxrInterface& iface, const char_t* path) {
    return iface.delegates.load_assembly(path, nullptr, nullptr);
}

int dotnet_get_function_pointer(
    HostfxrInterface& iface,
    const char_t* type_name, // Assembly qualified type name
    const char_t* method_name, // Public static method name compatible with delegateType
    const char_t* delegate_type_name, //Assembly qualified delegate type name or null,
                                        // or UNMANAGEDCALLERSONLY_METHOD if the method is marked with
                                        // the UnmanagedCallersOnlyAttribute.
    void** fptr
) {
    return iface.delegates.get_function_pointer(type_name, method_name, delegate_type_name, nullptr, nullptr, fptr);
}

extern "C" DLLEXPORT int test_internal_call(int x) {
    printf("test_internal_call(%d)\n", x);
    return x - 1;
}


extern "C" DLLEXPORT int* test_internal_ptr_return_call() {
    auto out = new int;
    *out = 6;
    return out;
}
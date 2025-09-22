#include "internal.hpp"

namespace NT {
__declspec(dllexport) int __cdecl engine_main(int argc, char* argv[]) {
    // HACK quick testy code

    HostfxrInterface iface;
    bool load_result = hostfxr_load_lib(iface);
    assert(load_result);
    printf("hostfxr loaded!\n");
    hostfxr_handle ctx;
    hostfxr_init_context(iface, &ctx);
    printf("hostfxr context initialized!\n");
    hostfxr_populate_delegates(ctx, iface);
    printf("delegates populated!\n");

    dotnet_load_assembly(iface, L"build/framework/NT.dll");
    printf("assembly loaded!\n");

    typedef int(CORECLR_DELEGATE_CALLTYPE *default_fn)(void* arg, int size);
    typedef int(CORECLR_DELEGATE_CALLTYPE *foo_fn)(void);
    typedef int(CORECLR_DELEGATE_CALLTYPE *bar_fn)(int);
    typedef int*(CORECLR_DELEGATE_CALLTYPE *mem_fn)(void);

    foo_fn foo;
    bar_fn bar;
    default_fn baz;
    mem_fn mem_test;

    struct BazArgs {
        float x;
        int y;
    } bazArgs;

    bazArgs.x = 3.14;
    bazArgs.y = -37;

    int rc;


    rc = dotnet_get_function_pointer(
        iface,
        L"NTF.Test, NT",
        L"Baz",
        nullptr,
        (void**)&baz
    );
    printf("got fn ptr baz: %d\n", rc);
    printf("NT.Test.Baz(~) => %d\n", baz((void*)&bazArgs, sizeof(BazArgs)));

    rc = dotnet_get_function_pointer(
        iface,
        L"NTF.Test, NT",
        L"Foo",
        L"NTF.FooFn, NT",
        (void**)&foo
    );
    printf("got fn ptr foo: %d\n", rc);
    printf("NT.Main.Foo() => %d\n", foo());

    dotnet_get_function_pointer(
        iface,
        L"NTF.Test, NT",
        L"Bar",
        L"NTF.BarFn, NT",
        (void**)&bar);
    printf("got fn ptr bar\n");
    printf("NT.Main.Bar(10) => %d\n", bar(10));


    dotnet_get_function_pointer(
        iface,
        L"NTF.Test, NT",
        L"MemTest",
        L"NTF.MemTestFn, NT",
        (void**)&mem_test);
    printf("got fn ptr mem_test\n");
    int* prodigal = mem_test();

    printf("value of prodigal is: %d\n", *prodigal);
    return 0;
}
}
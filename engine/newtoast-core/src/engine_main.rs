use crate::dotnet;


pub fn engine_main() {
    println!("PWD 2:\n  {}", std::env::current_dir().unwrap().display());

    let hostfxr_lib = dotnet::load_hostfxr();
    let mut dotnet = dotnet::create_context(&hostfxr_lib, "runtimeconfig.json");

    let mut fpath = std::env::current_dir().unwrap();
    fpath.push("build/framework/NT.dll");

    dotnet.load_assembly(fpath.to_str().unwrap());


    let foo_fn = dotnet.get_fn_pointer(
        "NTF.Test, NT",
        "Foo",
        "NTF.FooFn, NT",
    );

    let memtest_fn = dotnet.get_fn_pointer(
        "NTF.Test, NT",
        "MemTest",
        "NTF.MemTestFn, NT",
    );

    unsafe {
        let foo_out: i32 = foo_fn.call(());
        println!("Foo() -> {}", foo_out);
        let memtest_out: *mut i32 = memtest_fn.call(());
        println!("*MemTest() -> {}", *memtest_out);
    }
}

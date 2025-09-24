use crate::dotnet::{self, DotnetContext};


fn test_hostfxr() {

    let hostfxr_lib = dotnet::load_hostfxr();

    {

        let mut dotnet = dotnet::create_context(&hostfxr_lib, "runtimeconfig.json");

        let mut fpath = std::env::current_dir().unwrap();
        fpath.push("build/framework/NT.dll");

        dotnet.load_assembly(fpath.to_str().unwrap());

        unsafe {
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

            let foo_out: i32 = foo_fn.call(());
            println!("Foo() -> {}", foo_out);
            let memtest_out: *mut i32 = memtest_fn.call(());
            println!("*MemTest() -> {}", *memtest_out);
        }

        unsafe {
            let enqueue_fn = dotnet.get_fn_pointer(
                "NTF.Test, NT",
                "TestEnqueue",
                "NTF.EnqueueFn, NT",
            );

            let eq1 = std::sync::Arc::new(enqueue_fn);
            let eq2 = std::sync::Arc::clone(&eq1);
            let t1 = std::thread::spawn(move || {
                for i in 0..50{
                    eq1.call::<(i32), ()>((i));
                    std::thread::sleep(std::time::Duration::from_millis(5));
                }
            });
            let t2 = std::thread::spawn(move || {
                for i in 9950..10000 {
                    eq2.call::<(i32), ()>((i));
                    std::thread::sleep(std::time::Duration::from_millis(5));
                }
            });

            t2.join().unwrap();
            t1.join().unwrap();

            let dump_queue_fn = dotnet.get_fn_pointer(
                "NTF.Test, NT",
                "DumpQueue",
                "NTF.DumpQueueFn, NT",
            );

            dump_queue_fn.call::<(), ()>(());
        }
    }
}


pub fn engine_main() {
    println!("PWD 2:\n  {}", std::env::current_dir().unwrap().display());

    let hostfxr_lib = dotnet::load_hostfxr();
    let mut dotnet = dotnet::create_context(&hostfxr_lib, "runtimeconfig.json");

    let mut fpath = std::env::current_dir().unwrap();
    fpath.push("build/framework/NT.dll");

    dotnet.load_assembly(fpath.to_str().unwrap());

    core_loop(&hostfxr_lib, &mut dotnet);

}

// lifetime specifiers probably aren't necessary but being explicit might help avoid pits
fn core_loop<'lib, 'ctx>(lib: &'lib libloading::Library, dotnet: &'ctx mut DotnetContext<'lib>) {
    let mut frame = 0;
    loop {
        std::thread::sleep(std::time::Duration::from_secs(1));
        frame += 1;
        println!("Frame {}", frame);
        if frame > 10 { break };
    }
}
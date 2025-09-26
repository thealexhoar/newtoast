use std::{ffi::{CStr, CString}, mem::transmute};

use sdl3_sys::{error::SDL_GetError, events::{SDL_EventType, SDL_PollEvent, SDL_EVENT_QUIT}, init::{self, SDL_Init, SDL_InitFlags, SDL_Quit, SDL_INIT_VIDEO}, surface::SDL_DestroySurface, video::{SDL_CreateWindow, SDL_DestroyWindow, SDL_GL_CreateContext, SDL_GL_SwapWindow, SDL_GetWindowSurface, SDL_UpdateWindowSurface, SDL_WINDOW_OPENGL}};

use crate::{dotnet::{self, DotnetContext}, render::RenderContext};

#[repr(C)]
struct EntrypointSpecifierRaw {
    ns_name: *const i8,
    class_name: *const i8,
}

struct EntrypointSpecifier {
    ns_name: CString,
    class_name: CString,
}

impl From<EntrypointSpecifierRaw> for EntrypointSpecifier {
    fn from(raw: EntrypointSpecifierRaw) -> Self {
        unsafe {
            let ns_name = CStr::from_ptr(raw.ns_name).to_owned();
            let class_name = CStr::from_ptr(raw.class_name).to_owned();

            EntrypointSpecifier { ns_name, class_name }
        }
    }
}

fn setup_dotnet_runtime(dotnet: &mut DotnetContext) {
    let mut fpath = std::env::current_dir().unwrap();
    fpath.push("build/framework/NT.dll");

    dotnet.load_assembly(fpath.to_str().unwrap());

    unsafe {
        let find_entrypoint_fn: dotnet::DotnetFunctionPtr = dotnet.get_fn_pointer(
            "NTF.Test, NT",
            "FindEntrypoint",
            "NTF.FindEntrypointFn, NT");

        let mut entrypoint_specifier_raw = EntrypointSpecifierRaw {
            ns_name: std::ptr::null(),
            class_name: std::ptr::null(),
        };
        let entrypoint_res: bool = find_entrypoint_fn.call((&mut entrypoint_specifier_raw));
        // let entrypoint_raw: EntrypointSpecifierRaw = find_entrypoint_fn.call(());
        println!("found entrypoint? {}", entrypoint_res);
        let entrypoint_specifier = EntrypointSpecifier::from(entrypoint_specifier_raw);
        println!("  {} {}", entrypoint_specifier.ns_name.to_string_lossy(), entrypoint_specifier.class_name.to_string_lossy());
        // let entrypoint = EntrypointSpecifier::from(entrypoint_raw);
        // println!("Entrypoint: {}.{}", entrypoint.ns_name.to_str().unwrap(), entrypoint.class_name.to_str().unwrap());
    }
}


fn test_dotnet(dotnet: &mut DotnetContext) {
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


pub fn engine_main() {
    println!("PWD:\n  {}", std::env::current_dir().unwrap().display());

    let hostfxr_lib = dotnet::load_hostfxr();
    let mut dotnet = dotnet::create_context(&hostfxr_lib, "runtimeconfig.json");

    setup_dotnet_runtime(&mut dotnet);
    // test_dotnet(&mut dotnet);

    core_loop(&mut dotnet);
}

// lifetime specifiers probably aren't necessary but being explicit might help avoid pits
fn core_loop<'lib, 'ctx>(
    dotnet: &'ctx mut DotnetContext<'lib>
) {
    unsafe {
        // HACK this is extraordinarily cursed
        // but once it's chugging along I can start to make kinder interfaces
        let initflags = SDL_INIT_VIDEO;
        let init_result = SDL_Init(initflags);
        if !init_result {

            let sdl_err = CStr::from_ptr(SDL_GetError()).to_string_lossy().to_owned();
            println!("SDL_Init failed: {}", &sdl_err);
        }

        let mut render_context = RenderContext::new().expect("RenderContext::new failed");

        let mut tick_events = Vec::new();
        let mut should_exit = false;

        'gameloop: loop {
            // Handle input
            render_context.collect_events_into(&mut tick_events);
            for event in &tick_events {
                match SDL_EventType(event.etype) {
                    SDL_EVENT_QUIT => {
                        should_exit = true;
                    },
                    _ => {}
                }
            }
            tick_events.clear();

            render_context.imgui_frame(|ui| {
                ui.show_demo_window(&mut true);
            });

            render_context.render_frame();

            if should_exit {
                break 'gameloop;
            }
        }

    }
}
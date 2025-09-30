use std::{ffi::{CStr, CString}};
use sdl3_sys::{error::SDL_GetError, events::{SDL_EventType, SDL_EVENT_QUIT}, init::{SDL_Init, SDL_INIT_VIDEO}};

use crate::{dotnet::{self, DotnetContext}, render::RenderContext, runtime_interface::entrypoint::RuntimeEntrypoints, util::timing::InterpolatingTimer};


fn setup_dotnet_runtime(dotnet: &mut DotnetContext) -> Result<RuntimeEntrypoints, &'static str> {
    let mut fpath = std::env::current_dir().unwrap();
    fpath.push("build/framework/NT.dll");

    dotnet.load_assembly(fpath.to_str().unwrap());

        RuntimeEntrypoints::find_entrypoint_and_build(dotnet)
}

pub fn engine_main() {
    println!("PWD:\n  {}", std::env::current_dir().unwrap().display());

    let hostfxr_lib = dotnet::load_hostfxr();
    let mut dotnet = dotnet::create_context(&hostfxr_lib, "runtimeconfig.json");

    let runtime_interface = setup_dotnet_runtime(&mut dotnet)
        .expect("Failed to craete RuntimeEntrypoints");
    // test_dotnet(&mut dotnet);

    core_loop(&runtime_interface);
}

// lifetime specifiers probably aren't necessary but being explicit might help avoid pits
fn core_loop(runtime_interface: &RuntimeEntrypoints) {
    unsafe {
        // HACK this is extraordinarily cursed
        // but once it's chugging along I can start to make kinder interfaces

        // Initialize runtime here to pick up any config changes
        let foo: u8 = 21;
        runtime_interface.initialize(&foo);

        let initflags = SDL_INIT_VIDEO;
        let init_result = SDL_Init(initflags);
        if !init_result {
            let sdl_err = CStr::from_ptr(SDL_GetError()).to_string_lossy().to_owned();
            println!("SDL_Init failed: {}", &sdl_err);
        }

        let mut render_context = RenderContext::new().expect("RenderContext::new failed");

        // TODO initialize subsystems

        let mut tick_events = Vec::new();
        let mut should_exit = false;
        let mut timer = InterpolatingTimer::new(1.0 / 60.0);

        'gameloop: loop {
            // Handle time
            let (updates, alpha) = timer.tick();

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

            // TODO revisit the idea of passing dt like this at all
            for _ in 0..updates {
                runtime_interface.update(1.0 / 60.0);
            }

            // TODO figure out how to properly use dt
            runtime_interface.draw();

            render_context.imgui_frame(|ui| {
                ui.show_demo_window(&mut true);
            });

            render_context.render_frame();

            if should_exit || true {
                runtime_interface.shutdown();
                break 'gameloop;
            }
        }

    }
}
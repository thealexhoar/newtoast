mod gl_get_proc_address;
pub use gl_get_proc_address::*;

mod imgui_sdl;

use std::ffi::CStr;

use glow::{HasContext, NativeTexture};
use sdl3_sys::everything::*;

use crate::render::imgui_sdl::ImguiSdl;


#[derive(Clone, Debug)]
pub enum RenderError {
    WindowCreationFailed(String),
    GLSetupFailed(String),
    SetVsyncFailed(String)
}


pub struct RenderContext {
    window: *mut SDL_Window,
    renderer: *mut SDL_Renderer,
    gl_context: *mut SDL_GLContextState,
    gl: glow::Context,
    imgui: imgui::Context,
    imgui_sdl: ImguiSdl,
}

impl RenderContext {
    pub fn new() -> Result<Self, RenderError> {
        unsafe {
            //TODO config
            let title = "NEW TOAST\0";

            let load_gl_result = SDL_GL_LoadLibrary(std::ptr::null());
            if false == load_gl_result {
                let mut sdl_err = CStr::from_ptr(SDL_GetError()).to_string_lossy().into_owned();
                sdl_err.insert_str(0, "SDL_GL_LoadLibrary failed: ");
                return Err(RenderError::GLSetupFailed(sdl_err));
            }

            SDL_GL_SetAttribute(
                SDL_GLAttr::CONTEXT_PROFILE_MASK,
                SDL_GL_CONTEXT_PROFILE_CORE as i32,
            );

            SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3);
            SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 3);
            SDL_GL_SetAttribute(SDL_GL_CONTEXT_FLAGS, SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG);

            let mut window = std::ptr::null_mut();
            let mut renderer = std::ptr::null_mut();

            // TODO gl attrs

            let create_window_result = SDL_CreateWindowAndRenderer(
                title.as_ptr() as *const i8,
                960, 640,
                SDL_WINDOW_OPENGL,
                &mut window,
                &mut renderer
            );


            if false == create_window_result {
                let sdl_err = CStr::from_ptr(SDL_GetError()).to_string_lossy().into_owned();
                return Err(RenderError::WindowCreationFailed(sdl_err));
            }

            let gl_context = SDL_GL_CreateContext(window);

            if gl_context.is_null() {
                let mut sdl_err = CStr::from_ptr(SDL_GetError()).to_string_lossy().into_owned();
                sdl_err.insert_str(0, "SDL_GL_CreateContext failed: ");
                return Err(RenderError::GLSetupFailed(sdl_err));
            }

            let make_current_resut = SDL_GL_MakeCurrent(window, gl_context);
            if false == make_current_resut {
                let mut sdl_err = CStr::from_ptr(SDL_GetError()).to_string_lossy().into_owned();
                sdl_err.insert_str(0, "SDL_GL_MakeCurrent failed: ");
                return Err(RenderError::GLSetupFailed(sdl_err));
            }
            let gl = glow::Context::from_loader_function(gl_get_proc_address);


            let mut imgui = imgui::Context::create();
            imgui.set_ini_filename(None); // FIXME actual .ini file path

            let imgui_sdl = ImguiSdl::new(&gl, &mut imgui, window);

            //SDL configuration
            // let present_mode = sdl3_sys::gpu::SDL_GPUPresentMode::VSYNC;

            let set_vsync_result = SDL_SetRenderVSync(renderer, 1); // sync every 1th frame

            if false == set_vsync_result {
                let sdl_err = CStr::from_ptr(SDL_GetError()).to_string_lossy().into_owned();
                return Err(RenderError::SetVsyncFailed(sdl_err));
            }


            Ok(Self {
                window,
                renderer, //FIXME do I need this?
                gl_context,
                gl,
                imgui,
                imgui_sdl
            })
        }
    }

    pub fn collect_events(&mut self) -> Vec<SDL_Event> {
        let mut events = Vec::new();
        unsafe {
            let mut event = std::mem::zeroed();
            while SDL_PollEvent(&mut event) {
                self.imgui_sdl.handle_event(&mut self.imgui, &event);
                events.push(event);
            }
        }
        events
    }

    pub fn collect_events_into(&mut self, out_events: &mut Vec<SDL_Event>) {
        unsafe {
            let mut event = std::mem::zeroed();
            while SDL_PollEvent(&mut event) {
                self.imgui_sdl.handle_event(&mut self.imgui, &event);
                out_events.push(event);
            }
        }
    }

    pub fn imgui_frame<F>(&mut self, mut f: F)
        where F: FnMut(&mut imgui::Ui) -> ()
    {
        let ui = self.imgui_sdl.frame(&mut self.imgui, self.window);
        f(ui)
    }

    pub fn render_frame(&mut self) {
        unsafe {
            self.gl.clear(glow::COLOR_BUFFER_BIT);

            self.imgui_sdl.draw(&self.gl, &mut self.imgui);

            SDL_GL_SwapWindow(self.window);
        }
    }
}


impl Drop for RenderContext {
    fn drop(&mut self) {
        unsafe {
            SDL_DestroyWindow(self.window);
            SDL_Quit();
        }
    }
}
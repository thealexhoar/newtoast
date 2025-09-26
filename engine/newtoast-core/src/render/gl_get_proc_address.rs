use sdl3_sys::video::SDL_GL_GetProcAddress;



pub fn gl_get_proc_address(s: &str) -> *const std::ffi::c_void {
	unsafe {
        match SDL_GL_GetProcAddress(s.as_ptr() as *const i8) {
            Some(fptr) => fptr as *const _,
            None => std::ptr::null()
        }
    }
}

// TODO first verify whether this is actually safe
// pub fn caching_gl_proc_address_getter() ->

pub fn annotated_gl_proc_address_getter(tag: &str) -> impl Fn(&str) -> *const std::ffi::c_void + '_ {
    move |s| {
        let addr = gl_get_proc_address(s);
        if addr.is_null() {
            println!("{}: SDL_GL_GetProcAddress FAILED for {}", tag, s);
        } else {
            println!("{}: SDL_GL_GetProcAddress succeeded for {}", tag, s);
        }

        addr
    }
}
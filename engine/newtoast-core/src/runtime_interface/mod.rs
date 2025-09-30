use std::os::raw::c_void;

pub mod config;
pub mod entrypoint;
pub mod input;
pub mod render;
pub mod singletons;


#[no_mangle]
pub extern "C" fn test_internal_ptr_return_call() -> *const i32 {
    unsafe {
        let byte_ptr = std::alloc::alloc(std::alloc::Layout::new::<i32>());
        let ptr:*mut i32 = std::mem::transmute(byte_ptr);
        *ptr = 42;
        ptr
    }
}


#[no_mangle]
pub extern "C" fn test_modify_ref(u8_ref: &mut u8) {
    *u8_ref *= 2;
    println!("u8 ref after modification: {}", u8_ref);
}
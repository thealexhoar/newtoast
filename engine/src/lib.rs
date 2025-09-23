

#[no_mangle]
pub extern "C" fn test_internal_ptr_return_call() -> *const i32 {
    unsafe {
        let byte_ptr = std::alloc::alloc(std::alloc::Layout::new::<i32>());
        let ptr:*mut i32 = std::mem::transmute(byte_ptr);
        *ptr = 42;
        ptr
    }
}
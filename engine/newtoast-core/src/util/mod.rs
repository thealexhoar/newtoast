use std::{ffi::{CStr, CString}, mem::transmute};

use widestring::U16CString;

pub mod timing;


pub fn parse_hstr_wide(s: *const u16) -> Option<U16CString> {
    unsafe {
        if s.is_null() {
            None
        }
        else {
            Some(U16CString::from_ptr_str(s))
        }
    }
}
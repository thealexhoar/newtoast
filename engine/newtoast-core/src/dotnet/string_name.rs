use std::{collections::HashMap, ffi::CString};

use widestring::U16CString;


const STORAGE_MAX_BINS: usize = 8;
const STORAGE_MIN_BIN_SIZE_LOG2: usize = 7; // 128

// NOTE for now removal is not supported
struct StringNameStorage {
    data: [Option<Box<[String]>>; STORAGE_MAX_BINS],
    bins_used: usize,
    capacity: usize,
    free_indices: Vec<(usize, usize)>
}

impl StringNameStorage {
    pub fn new() -> Self {
        Self {
            data: Default::default(),
            bins_used: 0,
            capacity: 0,
            free_indices: Vec::new()
        }
    }

    pub fn insert() -> bool {
        todo!()
    }

    pub fn insert_wide() -> bool {
        todo!()
    }

    pub fn create_string_name<'storage>(&'storage mut self, data: &str) -> StringName<'storage> {
        todo!();
    }

    fn grow(&mut self) {
        if self.bins_used >= self.data.len() {
            panic!("StringNameStorage out of capacity");
        }

        let bin_size = 1 << (self.bins_used + STORAGE_MIN_BIN_SIZE_LOG2);
        // let new_bin = vec![]
        todo!();
    }
}


lazy_static::lazy_static! {
    static ref STRING_NAME_STORAGE: StringNameStorage = StringNameStorage::new();
    static ref STRING_NAME_MAP: HashMap<String, usize> = HashMap::new();
    static ref STRING_NAME_WIDE_MAP: HashMap<U16CString, usize> = HashMap::new();
}

#[repr(C)]
pub struct StringName<'storage> {
    foo: &'storage String
}



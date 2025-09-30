use lazy_static::lazy_static;
use widestring::{U16CStr, U16CString};

#[repr(C)]
pub struct RawInitConfig {
    pub window_title: *const i16,
    pub window_width: i32,
    pub window_height: i32,
    pub vsync: bool,
    pub windowed: bool,
}

pub struct InitConfig {
    pub window_title: String,
    pub window_width: i32,
    pub window_height: i32,
    pub vsync: bool,
    pub windowed: bool,
}

impl From<RawInitConfig> for InitConfig {
    fn from(raw: RawInitConfig) -> Self {
        unsafe {
            InitConfig {
                window_title: U16CString::from_ptr_str(raw.window_title as *const u16)
                    .to_string_lossy(),
                window_width: raw.window_width,
                window_height: raw.window_height,
                vsync: raw.vsync,
                windowed: raw.windowed,
            }
        }
    }
}

#[derive(Debug, Default)]
pub struct Config {

}


pub extern "C" fn set_init_config(config: InitConfig) {}
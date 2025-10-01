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

impl RawInitConfig {
    pub fn cook<F>(self, free_hstr: F) -> InitConfig
        where F: Fn(*const i16) -> ()
    {
        unsafe {
            let window_title = if self.window_title.is_null() {
                "NT".into()
            } else {
                let out = U16CString::from_ptr_str(self.window_title as *const u16)
                    .to_string_lossy();
                free_hstr(self.window_title);
                out
            };

            InitConfig {
                window_title,
                window_width: self.window_width,
                window_height: self.window_height,
                vsync: self.vsync,
                windowed: self.windowed,
            }
        }
    }
}

pub struct InitConfig {
    pub window_title: String,
    pub window_width: i32,
    pub window_height: i32,
    pub vsync: bool,
    pub windowed: bool,
}

#[derive(Debug, Default)]
pub struct Config {

}


pub extern "C" fn set_init_config(config: InitConfig) {}
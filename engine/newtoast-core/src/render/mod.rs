mod gl_get_proc_address;
use gl_get_proc_address::gl_get_proc_address;

mod imgui_sdl;

pub mod math_types;

mod render_context;
pub use render_context::*;

mod render_server;
pub use render_server::*;
/*
MIT License

Copyright (c) 2019 Stephan Schauerte

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

pub use imgui;

use imgui::{ConfigFlags, Context, Key as ImGuiKey, MouseCursor};
use imgui_opengl_renderer::Renderer;
use std::ffi::{CStr, CString};
use std::os::raw::{c_char, c_void};
use std::time::Instant;

use sdl3_sys::everything::*;

struct SdlClipboardBackend(*mut SDL_Window);

impl imgui::ClipboardBackend for SdlClipboardBackend {
	fn get(&mut self) -> Option<String> {
		unsafe {
			let ptr = SDL_GetClipboardText();
			if ptr.is_null() {
				return None;
			}
			let c_str = CStr::from_ptr(ptr);
			let string = c_str.to_string_lossy().into_owned();
			SDL_free(ptr as *mut c_void);
			Some(string)
		}
	}

	fn set(&mut self, value: &str) {
		let c_value = CString::new(value).unwrap();
		unsafe {
			SDL_SetClipboardText(c_value.as_ptr());
		}
	}
}

pub struct ImguiSdl {
	last_frame: Instant,
	mouse_press: [bool; 5],
	cursor_pos: (f32, f32),
	cursor: (MouseCursor, Option<SDL_SystemCursor>),//FIXME
	renderer: Renderer,
	window: *mut SDL_Window,
}

impl ImguiSdl {
	pub fn new(imgui: &mut Context, window: *mut SDL_Window) -> Self {
		unsafe {
			imgui.set_clipboard_backend(SdlClipboardBackend(window));
		}

		let io_mut = imgui.io_mut();

		// Map ImGui keys to SDL scancodes
		io_mut.key_map[ImGuiKey::Tab as usize] = SDL_SCANCODE_TAB.0 as u32;
		io_mut.key_map[ImGuiKey::LeftArrow as usize] = SDL_SCANCODE_LEFT.0 as u32;
		io_mut.key_map[ImGuiKey::RightArrow as usize] = SDL_SCANCODE_RIGHT.0 as u32;
		io_mut.key_map[ImGuiKey::UpArrow as usize] = SDL_SCANCODE_UP.0 as u32;
		io_mut.key_map[ImGuiKey::DownArrow as usize] = SDL_SCANCODE_DOWN.0 as u32;
		io_mut.key_map[ImGuiKey::PageUp as usize] = SDL_SCANCODE_PAGEUP.0 as u32;
		io_mut.key_map[ImGuiKey::PageDown as usize] = SDL_SCANCODE_PAGEDOWN.0 as u32;
		io_mut.key_map[ImGuiKey::Home as usize] = SDL_SCANCODE_HOME.0 as u32;
		io_mut.key_map[ImGuiKey::End as usize] = SDL_SCANCODE_END.0 as u32;
		io_mut.key_map[ImGuiKey::Insert as usize] = SDL_SCANCODE_INSERT.0 as u32;
		io_mut.key_map[ImGuiKey::Delete as usize] = SDL_SCANCODE_DELETE.0 as u32;
		io_mut.key_map[ImGuiKey::Backspace as usize] = SDL_SCANCODE_BACKSPACE.0 as u32;
		io_mut.key_map[ImGuiKey::Space as usize] = SDL_SCANCODE_SPACE.0 as u32;
		io_mut.key_map[ImGuiKey::Enter as usize] = SDL_SCANCODE_RETURN.0 as u32;
		io_mut.key_map[ImGuiKey::Escape as usize] = SDL_SCANCODE_ESCAPE.0 as u32;
		io_mut.key_map[ImGuiKey::A as usize] = SDL_SCANCODE_A.0 as u32;
		io_mut.key_map[ImGuiKey::C as usize] = SDL_SCANCODE_C.0 as u32;
		io_mut.key_map[ImGuiKey::V as usize] = SDL_SCANCODE_V.0 as u32;
		io_mut.key_map[ImGuiKey::X as usize] = SDL_SCANCODE_X.0 as u32;
		io_mut.key_map[ImGuiKey::Y as usize] = SDL_SCANCODE_Y.0 as u32;
		io_mut.key_map[ImGuiKey::Z as usize] = SDL_SCANCODE_Z.0 as u32;

		let renderer = Renderer::new(imgui, |s| {
			unsafe {
				let func_ptr = SDL_GL_GetProcAddress(s.as_ptr() as *const c_char).unwrap();
				std::mem::transmute(func_ptr)
			}
		});

		Self {
			last_frame: Instant::now(),
			mouse_press: [false; 5],
			cursor_pos: (0., 0.),
			cursor: (MouseCursor::Arrow, None),
			renderer,
			window,
		}
	}

	pub fn handle_event(&mut self, imgui: &mut Context, event: &SDL_Event) {
		unsafe {
			match event.etype {
				x if x == SDL_EVENT_MOUSE_BUTTON_DOWN.into() => {
					let button = event.button.button;
					let index = match button as i32 {
						SDL_BUTTON_LEFT => 0,
						SDL_BUTTON_RIGHT => 1,
						SDL_BUTTON_MIDDLE => 2,
						4 => 3, // X1
						5 => 4, // X2
						_ => 0,
					};
					self.mouse_press[index] = true;
					imgui.io_mut().mouse_down = self.mouse_press;
				}
				x if x == SDL_EVENT_MOUSE_BUTTON_UP.into() => {
					let button = event.button.button;
					let index = match button as i32 {
						SDL_BUTTON_LEFT => 0,
						SDL_BUTTON_RIGHT => 1,
						SDL_BUTTON_MIDDLE => 2,
						4 => 3, // X1
						5 => 4, // X2
						_ => 0,
					};
					self.mouse_press[index] = false;
					imgui.io_mut().mouse_down = self.mouse_press;
				}
				x if x == SDL_EVENT_MOUSE_MOTION.into() => {
					let x = event.motion.x as f32;
					let y = event.motion.y as f32;
					imgui.io_mut().mouse_pos = [x, y];
					self.cursor_pos = (x, y);
				}
				x if x == SDL_EVENT_MOUSE_WHEEL.into() => {
					let y = event.wheel.y as f32;
					imgui.io_mut().mouse_wheel = y;
				}
				x if x == SDL_EVENT_TEXT_INPUT.into() => {
					let text = CStr::from_ptr(event.text.text).to_string_lossy();
					for ch in text.chars() {
						imgui.io_mut().add_input_character(ch);
					}
				}
				x if x == SDL_EVENT_KEY_DOWN.into() || x == SDL_EVENT_KEY_UP.into() => {
					let pressed = event.etype == SDL_EVENT_KEY_DOWN.into();
					let scancode_int: std::os::raw::c_int = event.key.scancode.into();
					let scancode = scancode_int as usize;
					if scancode < imgui.io().keys_down.len() {
						imgui.io_mut().keys_down[scancode] = pressed;
					}
					Self::set_mod(imgui, event.key.r#mod);
				}
				_ => {}
			}
		}
	}

	pub fn frame<'a>(&mut self, imgui: &'a mut Context) -> &'a mut imgui::Ui {
		let io = imgui.io_mut();

		let now = Instant::now();
		let delta = now - self.last_frame;
		let delta_s = delta.as_secs() as f32 + delta.subsec_nanos() as f32 / 1_000_000_000.0;
		self.last_frame = now;
		io.delta_time = delta_s;

		unsafe {
			let mut w = 0;
			let mut h = 0;
			SDL_GetWindowSize(self.window, &mut w, &mut h);
			io.display_size = [w as f32, h as f32];
		}

		let ui = imgui.frame();

		let io = ui.io();
		if !io.config_flags.contains(ConfigFlags::NO_MOUSE_CURSOR_CHANGE) {
			match ui.mouse_cursor() {
				Some(mouse_cursor) if !io.mouse_draw_cursor => {
					unsafe {
						SDL_SetWindowMouseGrab(self.window, false);
						let cursor = match mouse_cursor {
							MouseCursor::TextInput => SDL_SYSTEM_CURSOR_TEXT,
							MouseCursor::ResizeNS => SDL_SYSTEM_CURSOR_NS_RESIZE,
							MouseCursor::ResizeEW => SDL_SYSTEM_CURSOR_EW_RESIZE,
							MouseCursor::Hand => SDL_SYSTEM_CURSOR_POINTER,
							_ => SDL_SYSTEM_CURSOR_DEFAULT,
						};
						let sdl_cursor = SDL_CreateSystemCursor(cursor);
						SDL_SetCursor(sdl_cursor);
						if self.cursor.1 != Some(cursor) {
							self.cursor.1 = Some(cursor);
							self.cursor.0 = mouse_cursor;
						}
					}
				}
				_ => {
					self.cursor.0 = MouseCursor::Arrow;
					self.cursor.1 = None;
					unsafe { SDL_HideCursor(); }
				}
			}
		}

		ui
	}

	pub fn draw<'ui>(&mut self, imgui: &mut Context) {
		self.renderer.render(imgui);
	}

	fn set_mod(imgui: &mut Context, modifier: SDL_Keymod) {
		// SDL3 uses bitflags for modifiers
		let ctrl = (modifier & SDL_KMOD_CTRL) != 0;
		let alt = (modifier & SDL_KMOD_ALT) != 0;
		let shift = (modifier & SDL_KMOD_SHIFT) != 0;
		let super_ = (modifier & SDL_KMOD_GUI) != 0;
		let io = imgui.io_mut();
		io.key_ctrl = ctrl;
		io.key_alt = alt;
		io.key_shift = shift;
		io.key_super = super_;
	}
}

use crate::render::{Id, RenderServer, Transform2D};


#[no_mangle]
extern "C" fn render_server_renderable_2d_create(render_server: &mut RenderServer) -> Id {
    render_server.renderable_2d_create()
}

#[no_mangle]
extern "C" fn render_server_renderable_2d_destroy(render_server: &mut RenderServer, id: Id) {
    render_server.renderable_2d_destroy(id)
}

#[no_mangle]
extern "C" fn render_server_renderable_2d_set_transform(render_server: &mut RenderServer, id: Id, transform: Transform2D) {
    render_server.renderable_2d_set_transform(id, transform)
}

#[no_mangle]
extern "C" fn render_server_renderable_2d_set_target(render_server: &mut RenderServer, id: Id) -> Id {
    render_server.renderable_2d_set_target(id)
}

#[no_mangle]
extern "C" fn render_server_renderable_2d_set_target_with_target(render_server: &mut RenderServer, id: Id, target: Id) {
    render_server.renderable_2d_set_target_with_target(id, target)
}

#[no_mangle]
extern "C" fn render_server_material_2d_create(render_server: &mut RenderServer) -> Id {
    render_server.material_2d_create()
}

#[no_mangle]
extern "C" fn render_server_material_2d_destroy(render_server: &mut RenderServer, id: Id) {
    render_server.material_2d_destroy(id)
}

#[no_mangle]
extern "C" fn render_server_material_2d_set_shader(render_server: &mut RenderServer, id: Id, shader: Id) {
    render_server.material_2d_set_shader(id, shader)
}

// TODO render_server_material_2d_set_shader_param_
// float, int, ...

#[no_mangle]
extern "C" fn render_server_renderable_2d_add_surface(render_server: &mut RenderServer, renderable: Id, material: Id, mesh: Id) -> Id {
    render_server.renderable_2d_add_surface(renderable, material, mesh)
}

#[no_mangle]
extern "C" fn render_server_renderable_2d_clear_surfaces(render_server: &mut RenderServer, renderable: Id) {
    render_server.renderable_2d_clear_surfaces(renderable)
}

// TODO render_server_mesh_2d_create(render_server: &mut RenderServer, ...) -> Id

#[no_mangle]
extern "C" fn render_server_mesh_2d_destroy(render_server: &mut RenderServer, id: Id) {
    render_server.mesh_2d_destroy(id)
}

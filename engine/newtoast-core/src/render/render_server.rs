use std::collections::HashMap;

use smallvec::SmallVec;


pub type Id = u32;
pub type Transform2D = u32; // FIXME placeholder

struct Renderable2D {
    transform: Transform2D,
    surfaces: SmallVec<[(Id, Id); 3]>
}

enum UniformBinding {
    Float(f32),
    Int(i32),
}

struct Material2D {
    shader: Id,
    uniform_bindings: HashMap<String, UniformBinding>,
}

pub struct RenderServer {
    // TODO
    // - storage for GPU assets
    // - render command buffer

    next_id: Id,

    renderables_2d: HashMap<Id, Renderable2D>,
    materials_2d: HashMap<Id, ()>,
    shaders_2d: HashMap<Id, ()>,
    meshes_2d: HashMap<Id, ()>,

    render_targets: HashMap<Id, ()>,
}

impl RenderServer {
    pub fn new() -> Self {
        Self {
            next_id: 1,
            renderables_2d: HashMap::new(),
            materials_2d: HashMap::new(),
            shaders_2d: HashMap::new(),
            meshes_2d: HashMap::new(),
            render_targets: HashMap::new(),
        }
    }

    pub fn renderable_2d_create(&mut self) -> Id {
        let id = self.next_id;
        self.next_id += 1;
        self.renderables_2d.insert(id, Renderable2D {
            transform: 0, // TODO identity
            surfaces: SmallVec::new(),
        });
        id
    }

    pub fn renderable_2d_destroy(&mut self, id: Id) {
        if let Some(renderable) = self.renderables_2d.remove(&id) {
            // TODO cleanup?
        }
    }

    pub fn renderable_2d_set_transform(&mut self, id: Id, transform: Transform2D) {
        todo!()
    }

    pub fn renderable_2d_set_target(&self, id: Id) -> Id {
        todo!()
    }

    pub fn renderable_2d_set_target_with_target(&mut self, id: Id, target: Id) {
        todo!()
    }

    pub fn material_2d_create(&mut self) -> Id {
        todo!()
    }

    pub fn material_2d_destroy(&mut self, id: Id) {
        todo!()
    }

    pub fn material_2d_set_shader(&mut self, id: Id, shader: Id) {
        todo!()
    }

    pub fn renderable_2d_add_surface(&mut self, renderable: Id, material: Id, mesh: Id) -> Id {
        todo!()
    }

    pub fn renderable_2d_clear_surfaces(&mut self, renderable: Id) {
        todo!()
    }

    pub fn mesh_2d_destroy(&mut self, id: Id) {
        todo!()
    }
}
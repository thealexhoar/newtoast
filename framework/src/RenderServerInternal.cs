using System.Runtime.InteropServices;

namespace NTF
{
    public static class RenderServerInternal
    {
        private const string LIB_NEWTOAST_CORE = "newtoast_core";

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_renderable_2d_create")]
        internal static partial uint Renderable2DCreate(nint renderServer);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_renderable_2d_destroy")]
        internal static partial void Renderable2DDestroy(nint renderServer, uint id);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_renderable_2d_set_transform")]
        internal static partial void Renderable2DSetTransform(nint renderServer, uint id, uint transform);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_renderable_2d_set_target")]
        internal static partial uint Renderable2DSetTarget(nint renderServer, uint id);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_renderable_2d_set_target_with_target")]
        internal static partial void Renderable2DSetTargetWithTarget(nint renderServer, uint id, uint target);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_material_2d_create")]
        internal static partial uint Material2DCreate(nint renderServer);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_material_2d_destroy")]
        internal static partial void Material2DDestroy(nint renderServer, uint id);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_material_2d_set_shader")]
        internal static partial void Material2DSetShader(nint renderServer, uint id, uint shader);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_renderable_2d_add_surface")]
        internal static partial uint Renderable2DAddSurface(nint renderServer, uint renderable, uint material, uint mesh);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_renderable_2d_clear_surfaces")]
        internal static partial void Renderable2DClearSurfaces(nint renderServer, uint renderable);

        [LibraryImport(LIB_NEWTOAST_CORE, EntryPoint = "render_server_mesh_2d_destroy")]
        internal static partial void Mesh2DDestroy(nint renderServer, uint id);
    }
}
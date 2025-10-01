using System;

namespace NTF;
public static class RenderServer
{
    internal static nint BoundRenderServer;
    internal static bool IsRenderServerBound => BoundRenderServer != nint.Zero;

    public static uint Renderable2DCreate()
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        return RenderServerInternal.Renderable2DCreate(BoundRenderServer);
    }

    public static void Renderable2DDestroy(uint id)
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        RenderServerInternal.Renderable2DDestroy(BoundRenderServer, id);
    }

    public static void Renderable2DSetTransform(uint id, uint transform)
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        RenderServerInternal.Renderable2DSetTransform(BoundRenderServer, id, transform);
    }

    public static uint Renderable2DSetTarget(uint id)
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        return RenderServerInternal.Renderable2DSetTarget(BoundRenderServer, id);
    }

    public static void Renderable2DSetTargetWithTarget(uint id, uint target)
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        RenderServerInternal.Renderable2DSetTargetWithTarget(BoundRenderServer, id, target);
    }

    public static uint Material2DCreate()
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        return RenderServerInternal.Material2DCreate(BoundRenderServer);
    }

    public static void Material2DDestroy(uint id)
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        RenderServerInternal.Material2DDestroy(BoundRenderServer, id);
    }

    public static void Material2DSetShader(uint id, uint shader)
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        RenderServerInternal.Material2DSetShader(BoundRenderServer, id, shader);
    }

    public static uint Renderable2DAddSurface(uint renderable, uint material, uint mesh)
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        return RenderServerInternal.Renderable2DAddSurface(BoundRenderServer, renderable, material, mesh);
    }

    public static void Renderable2DClearSurfaces(uint renderable)
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        RenderServerInternal.Renderable2DClearSurfaces(BoundRenderServer, renderable);
    }

    public static void Mesh2DDestroy(uint id)
    {
        if (!IsRenderServerBound)
            throw new InvalidOperationException("RenderServer is not bound.");

        RenderServerInternal.Mesh2DDestroy(BoundRenderServer, id);
    }
}
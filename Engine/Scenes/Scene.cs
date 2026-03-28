using ErandevEngine.Utilities;
using OpenTK.Mathematics;

namespace ErandevEngine.Scenes;

public abstract class Scene(string name)
{
    public readonly string Name = name;
    public bool IsLoaded { get; protected set; }

    private event VoidDelegate? LoadEvent;
    private event VoidDelegate? UnloadEvent;
    private event SizeDelegate? WindowResizeEvent;
    private event SecondsDelegate? UpdateEvent;
    private event SecondsDelegate? RenderEvent;

    internal void Load()
    {
        if (IsLoaded) return;

        LoadEvent += OnLoad;
        WindowResizeEvent += OnWindowResize;
        UpdateEvent += OnUpdate;
        RenderEvent += OnRender;
        UnloadEvent += OnUnload;

        LoadEvent?.Invoke();

        IsLoaded = true;
    }

    internal void WindowResize(Vector2i size)
    {
        if (!IsLoaded) return;

        WindowResizeEvent?.Invoke(size);
    }

    internal void Update(double elapsedSeconds)
    {
        if (!IsLoaded) return;

        UpdateEvent?.Invoke(elapsedSeconds);
    }

    internal void Render(double elapsedSeconds)
    {
        if (!IsLoaded) return;

        RenderEvent?.Invoke(elapsedSeconds);
    }

    internal void Unload()
    {
        if (!IsLoaded) return;

        LoadEvent -= OnLoad;
        WindowResizeEvent -= OnWindowResize;
        UpdateEvent -= OnUpdate;
        RenderEvent -= OnRender;

        UnloadEvent?.Invoke();

        UnloadEvent -= OnUnload;

        IsLoaded = false;
    }

    protected abstract void OnLoad();
    protected abstract void OnWindowResize(Vector2i size);
    protected abstract void OnUpdate(double elapsedSeconds);
    protected abstract void OnRender(double elapsedSeconds);
    protected abstract void OnUnload();
}
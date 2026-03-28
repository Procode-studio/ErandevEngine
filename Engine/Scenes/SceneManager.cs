using OpenTK.Mathematics;

namespace ErandevEngine.Scenes;

public class SceneManager
{
    private readonly List<Scene> _scenes = [];
    private Scene? _selectedScene;

    public void Add(Scene scene)
    {
        if (!_scenes.Exists(s => s.Name == scene.Name))
        {
            _scenes.Add(scene);
        }
    }

    public void AddMany(IEnumerable<Scene> scenes)
    {
        foreach (Scene scene in scenes)
        {
            Add(scene);
        }
    }

    public void Remove(Scene scene)
    {
        if (_selectedScene == scene)
        {
            _selectedScene = null;
        }

        scene.Unload();
        _scenes.Remove(scene);
    }

    public void Remove(string name)
    {
        Scene? scene = _scenes.Find(s => s.Name == name);
        if (scene != null)
        {
            Remove(scene);
        }
    }

    public void RemoveMany(IEnumerable<Scene> scenes)
    {
        foreach (Scene scene in scenes)
        {
            Remove(scene);
        }
    }

    public void Select(string name)
    {
        if (_selectedScene?.Name == name && _selectedScene.IsLoaded) return;

        _selectedScene?.Unload();
        _selectedScene = _scenes.Find(s => s.Name == name);
        _selectedScene?.Load();
    }

    public void ResizeSelected(Vector2i size)
    {
        _selectedScene?.WindowResize(size);
    }

    public void UpdateSelected(double elapsedSeconds)
    {
        _selectedScene?.Update(elapsedSeconds);
    }

    public void RenderSelected(double elapsedSeconds)
    {
        _selectedScene?.Render(elapsedSeconds);
    }

    public void UnloadSelected()
    {
        _selectedScene?.Unload();
    }
}
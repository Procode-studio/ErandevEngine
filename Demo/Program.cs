using Demo.Scenes;
using ErandevEngine.Core;
using OpenTK.Mathematics;

Vector2i WindowSize = new(1024, 768);
Erandev.Engine.CreateWindow(WindowSize, "Demo", 60);
Erandev.Engine.Scenes.Add(new SceneOne("1"));
Erandev.Engine.Scenes.Add(new SceneTwo("2"));
Erandev.Engine.Scenes.Select("1");
Erandev.Engine.Run();
using Raylib_CsLo;
using JustWind.Entities;
using JustWind.Systems;
using JustWind.Components;

public class Engine
{
    public List<Entity> Entities = new List<Entity>();
    public List<JustWind.Systems.System> Systems = new List<JustWind.Systems.System>();
    public Entity Singleton { get; set; }

    public Camera2D Camera { get; set; }

    public Engine()
    {
        Systems.Add(new RenderingSystem(this));
        Systems.Add(new UiSystem(this));
        Systems.Add(new GenerationSystem(this));
        Systems.Add(new AiMovementSystem(this));
        Systems.Add(new ControllableSystem(this));
        Systems.Add(new ActionSystem(this));
        Systems.Add(new SoundSystem(this));
        Systems.Add(new StateManagerSystem(this));
        Systems.Add(new AnimationSystem(this));

        var singleton = new Entity();
        singleton.Components.Add(new Singleton { State = GameState.Menu });
        Singleton = singleton;
    }
    public void Run()
    {
        Raylib.InitWindow(1280, 720, "It's just the wind...");
        Raylib.SetTargetFPS(30);

        var cam = new Camera2D();
        cam.zoom = 1;
        Camera = cam;
        /// WHYYYY ^

        foreach (var system in Systems)
        {
            system.Load();
        }

        while (!Raylib.WindowShouldClose())
        {
            GameLoop();
        }
        Raylib.CloseWindow();
    }


    public void GameLoop()
    {
        Raylib.BeginDrawing();
        Raylib.BeginMode2D(Camera);
        Raylib.ClearBackground(Raylib.WHITE);

        foreach (var system in Systems)
        {
            system.Update(Entities);
        }

        Raylib.EndMode2D();
        Raylib.EndDrawing();
    }
}
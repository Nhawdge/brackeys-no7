using Raylib_CsLo;
using JustWind.Entities;
using JustWind.Systems;
using JustWind.Components;

public class Engine
{
    public List<Entity> Entities = new List<Entity>();
    public List<JustWind.Systems.System> Systems = new List<JustWind.Systems.System>();
    public List<JustWind.Systems.System> NoCameraSystems = new List<JustWind.Systems.System>();
    public Entity Singleton;
    public Camera2D Camera;

    public Engine()
    {
        Systems.Add(new GenerationSystem(this));
        Systems.Add(new AiMovementSystem(this));
        Systems.Add(new HouseSafetySystem(this));
        Systems.Add(new SoundSystem(this));
        Systems.Add(new ControllableSystem(this));
        Systems.Add(new StateManagerSystem(this));
        Systems.Add(new AnimationSystem(this));
        Systems.Add(new CameraSystem(this));
        Systems.Add(new RenderingSystem(this));
        Systems.Add(new ActionSystem(this));

        NoCameraSystems.Add(new UiSystem(this));

        var singleton = new Entity();
        singleton.Components.Add(new Singleton { State = GameState.Menu });
        Singleton = singleton;
    }
    public void Run()
    {
        Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
        Raylib.SetWindowIcon(Raylib.LoadImage("src/Assets/menu/icon.png"));

        Raylib.InitWindow(1280, 720, "It's just the wind...");

        Raylib.SetTargetFPS(240);
        //Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_UNDECORATED);

        Camera = new Camera2D();
        Camera.zoom = 1;

        foreach (var system in Systems)
        {
            system.Load();
        }
        foreach (var system in NoCameraSystems)
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

        foreach (var system in NoCameraSystems)
        {
            system.Update(Entities);
        }

        Raylib.EndDrawing();
    }
}
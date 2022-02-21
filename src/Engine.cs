using Raylib_CsLo;
using ZombieEscape.Entities;
using ZombieEscape.Systems;
using ZombieEscape.Components;

public class Engine
{
    public List<Entity> Entities = new List<Entity>();
    public List<ZombieEscape.Systems.System> Systems = new List<ZombieEscape.Systems.System>();
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

        var singleton = new Entity();
        singleton.Components.Add(new Singleton());
        Singleton = singleton;
    }
    public void Run()
    {
        Raylib.InitWindow(1280, 720, "Zombie Escape");
        Raylib.SetTargetFPS(30);
        Camera = new Camera2D();
        var cam = Camera;
        cam.zoom = 1;
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
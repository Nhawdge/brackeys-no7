using System.ComponentModel;
using System.Numerics;
using JustWind.Components;
using JustWind.Entities;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Systems
{
    public class UiSystem : Systems.System
    {
        public UiSystem(Engine engine) : base(engine)
        {
        }

        public override void Load()
        {
        }

        public override void Update(List<Entity> allEntities)
        {
            var mousePos = Raylib.GetMousePosition();

            var singleton = Engine.Singleton.GetComponent<Singleton>();
            if (singleton.State == GameState.Menu)
            {
                var startRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 - 100, 200, 50);
                RayGui.GuiButton(startRect, "Start");

                var howToRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2, 200, 50);
                RayGui.GuiButton(howToRect, "How to Play");

                var creditsRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 + 100, 200, 50);
                RayGui.GuiButton(creditsRect, "Credits");

                var exitRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 + 200, 200, 50);
                RayGui.GuiButton(exitRect, "Exit");

                if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, startRect))
                    {
                        singleton.State = GameState.Game;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, howToRect))
                    {
                        singleton.State = GameState.MenuHowToPlay;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, creditsRect))
                    {
                        singleton.State = GameState.MenuCredits;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, exitRect))
                    {
                        singleton.State = GameState.Exit;
                    }
                }
            }
            if (singleton.State == GameState.MenuHowToPlay)
            {
                var rect = new Rectangle(100, 100, GetScreenWidth() - 200, GetScreenHeight() - 200);
                RayGui.GuiPanel(rect);
                RayGui.GuiTextBox(rect, "1. Defend the home at all costs until the humans return", 72, false);

                var backRect = new Rectangle(GetScreenWidth() - 50, 10, MeasureText("Back", 20), 20);
                RayGui.GuiTextBox(backRect, "Back", 20, false);
                if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, backRect))
                    {
                        singleton.State = GameState.Menu;

                    }
                }
            }
            if (singleton.State == GameState.MenuCredits)
            {
                var rect = new Rectangle(100, 100, GetScreenWidth() - 200, GetScreenHeight() - 200);
                RayGui.GuiPanel(rect);
                RayGui.GuiTextBox(rect, "Coding - John Pavek - @nhawdge\nArt - Aaron - @AaronJLael", 72, false);

                var backRect = new Rectangle(GetScreenWidth() - 50, 10, MeasureText("Back", 20), 20);
                RayGui.GuiTextBox(backRect, "Back", 20, false);
                if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, backRect))
                    {
                        singleton.State = GameState.Menu;
                    }
                }
            }
            if (singleton.State == GameState.Exit)
            {
                Raylib.EndDrawing();
                Raylib.CloseWindow();
                Environment.Exit(0);
            }
            if (singleton.State == GameState.Game)
            {
                var corner = new Vector2((GetScreenWidth() - 75), 10);
                var offset = GetScreenToWorld2D(corner, Engine.Camera);

                var pauseRect = new Rectangle(corner.X, corner.Y, 60, 25);
                RayGui.GuiButton(pauseRect, "[P]aws");
                if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, pauseRect))
                    {
                        singleton.State = GameState.Paused;
                    }
                }
            }
            else if (singleton.State == GameState.Paused)
            {
                var center = new Vector2((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 - 25);
                var offset = GetWorldToScreen2D(center, Engine.Camera);

                var resumeRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 - 100, 200, 50);
                RayGui.GuiButton(resumeRect, "Resume");


                if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, resumeRect))
                    {
                        singleton.State = GameState.Game;
                    }
                }
            }
        }
    }
}
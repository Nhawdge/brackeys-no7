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
        public Texture MenuTexture;

        public UiSystem(Engine engine) : base(engine)
        {
        }

        public override void Load()
        {
            MenuTexture = LoadTexture("src/Assets/scene/cover.png");

        }

        public override void Update(List<Entity> allEntities)
        {
            var mousePos = Raylib.GetMousePosition();

            var singleton = Engine.Singleton.GetComponent<Singleton>();
            if (singleton.State == GameState.Menu)
            {
                DrawBackground();

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
                DrawText(GetFPS().ToString(), 10, 10, 20, GREEN);
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
                var percent = ((float)singleton.HouseSafety / (float)singleton.MaxHouseSafety);
                var textToDraw = (percent ).ToString("0%");
                var width = percent * 200;

                DrawRectangle(GetScreenWidth() / 2 - 105, 5, 210, 30, Raylib.BLACK);
                DrawRectangle(GetScreenWidth() / 2 - 100, 10, (int)width, 20, Raylib.RED);
                DrawText($"{textToDraw}", (GetScreenWidth() / 2) - (MeasureText(textToDraw, 12) / 2), 10, 20, Raylib.WHITE);

            }
            else if (singleton.State == GameState.Paused)
            {
                var resumeRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 - 100, 200, 50);
                RayGui.GuiButton(resumeRect, "Resume");

                var mainMenuRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 + 0, 200, 50);
                RayGui.GuiButton(mainMenuRect, "Main Menu");

                var exitRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 + 100, 200, 50);
                RayGui.GuiButton(exitRect, "Exit");

                if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, resumeRect))
                    {
                        singleton.State = GameState.Game;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, mainMenuRect))
                    {
                        singleton.State = GameState.Menu;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, exitRect))
                    {
                        singleton.State = GameState.Exit;
                    }
                }
            }
        }
        private void DrawBackground()
        {
            var bgMenuSourceRect = new Rectangle(0, 0, MenuTexture.width, MenuTexture.height);
            var bgMenuDestinationRect = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            DrawTexturePro(MenuTexture, bgMenuSourceRect, bgMenuDestinationRect, new Vector2(0), 0f, Raylib.WHITE);
        }
    }
}
using System.Numerics;
using JustWind.Components;
using JustWind.Entities;
using JustWind.Netcode;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Systems
{
    public class UiSystem : Systems.System
    {
        public Texture MenuTexture;

        private string message = string.Empty;
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

                //var howToRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2, 200, 50);
                //RayGui.GuiButton(howToRect, "How to Play");
                var serverRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2, 200, 50);
                RayGui.GuiButton(serverRect, "Host a Server");

                // var creditsRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 + 100, 200, 50);
                // RayGui.GuiButton(creditsRect, "Credits");

                var serverJoinRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 + 100, 200, 50);
                RayGui.GuiButton(serverJoinRect, "Join a Server");

                var exitRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 + 200, 200, 50);
                RayGui.GuiButton(exitRect, "Exit");

                if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, startRect))
                    {
                        singleton.State = GameState.Game;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, serverRect))
                    {
                        singleton.State = GameState.ServerHost;
                        Engine.Network = new NetServer(Engine);
                        Engine.NetworkThread = new Thread(() => Engine.Network.Start());
                        Engine.NetworkThread.Start();
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, serverJoinRect))
                    {
                        singleton.State = GameState.ServerJoin;
                        Engine.Network = new NetClient(Engine);
                        Engine.NetworkThread = new Thread(() => Engine.Network.Start());
                        Engine.NetworkThread.Start();
                    }
                    // if (Raylib.CheckCollisionPointRec(mousePos, howToRect))
                    // {
                    //     singleton.State = GameState.MenuHowToPlay;
                    // }
                    // if (Raylib.CheckCollisionPointRec(mousePos, creditsRect))
                    // {
                    //     singleton.State = GameState.MenuCredits;
                    // }
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
                RayGui.GuiTextBox(rect, "Defend the home at all costs until the humans return \n\nControls: \nBark: Left Mouse Button \nGrowl: Right Mouse Button", 72, false);

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
            if (singleton.State is GameState.ServerHost or GameState.ServerJoin)
            {
                var key = GetKeyPressed();
                if (key > 0)
                {
                    var keyPress = GetCharPressed();
                    if (keyPress > 0)
                    {
                        var keyPressed = (char)keyPress;
                        message += keyPressed;
                        Console.WriteLine($"{key}, {keyPressed}");
                    }
                    if (key == (int)KeyboardKey.KEY_ENTER)
                    {
                        this.Engine.Network.SendString(message);
                        Console.WriteLine("entered");
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
                DrawText($"FPS: {GetFPS().ToString()}", 10, 10, 20, GREEN);
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
                var textToDraw = (percent).ToString("0%");
                var width = percent * 200;

                DrawRectangle(GetScreenWidth() / 2 - 105, 5, 210, 30, Raylib.BLACK);
                DrawRectangle(GetScreenWidth() / 2 - 100, 10, (int)width, 20, Raylib.RED);
                DrawText($"{textToDraw}", (GetScreenWidth() / 2) - (MeasureText(textToDraw, 12) / 2), 10, 20, Raylib.WHITE);

                var stats = singleton.Stats;
                DrawText($"Time Remaining: {(stats.RoundDuration - stats.RoundTimer).ToString("0.0")}", 10, 40, 20, GREEN);

                DrawText($"Round: {stats.Round}", 10, 70, 20, GREEN);


            }
            else if (singleton.State == GameState.Paused)
            {
                Raylib.DrawText("Paws'd", Raylib.GetScreenWidth() / 2 - Raylib.MeasureText("Paws'd", 50) / 2, 100, 50, Raylib.BLUE);

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
            else if (singleton.State == GameState.GameLoss)
            {
                DrawBackground();

                Raylib.DrawText("You Lose!", Raylib.GetScreenWidth() / 2 - Raylib.MeasureText("You Lose!", 50) / 2, 100, 50, Raylib.RED);

                var startRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 - 100, 200, 50);
                RayGui.GuiButton(startRect, "Play Again");

                var quitRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2, 200, 50);
                RayGui.GuiButton(quitRect, "Rage Quit");

                if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, startRect))
                    {
                        singleton.State = GameState.Game;
                        singleton.HouseSafety = singleton.MaxHouseSafety;
                        singleton.LastSpawnTime = 0;
                        singleton.Stats.Round = 1;
                        singleton.Stats.RoundDuration = 90;
                        singleton.Stats.RoundTimer = 0;

                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, quitRect))
                    {
                        singleton.State = GameState.Exit;
                    }
                }
            }
            else if (singleton.State == GameState.GameWin)
            {
                DrawBackground();

                Raylib.DrawText("You Win!", Raylib.GetScreenWidth() / 2 - Raylib.MeasureText("You Win!", 50) / 2, 100, 50, Raylib.GREEN);

                var nextRoundRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2 - 100, 200, 50);
                RayGui.GuiButton(nextRoundRect, "Next Round");

                var quitRect = new Rectangle((GetScreenWidth() / 2 - 100), GetScreenHeight() / 2, 200, 50);
                RayGui.GuiButton(quitRect, "Rage Quit");

                if (Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON))
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, nextRoundRect))
                    {
                        singleton.State = GameState.NextRound;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, quitRect))
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
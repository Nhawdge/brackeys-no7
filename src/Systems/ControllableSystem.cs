using System.Numerics;
using JustWind.Components;
using JustWind.Entities;
using static Raylib_CsLo.Raylib;
using Raylib_CsLo;
using JustWind.Helpers;

namespace JustWind.Systems
{
    public class ControllableSystem : Systems.System
    {
        public ControllableSystem(Engine engine) : base(engine)
        {
        }

        public override void Load()
        {
        }

        public override void Update(List<Entity> allEntities)
        {
            var singleton = Engine.Singleton.GetComponent<Singleton>();
            if (singleton.State != GameState.Game)
            {
                return;
            }

            var player = allEntities.Find(x => x.HasTypes(typeof(Controllable), typeof(Position), typeof(Render)));
            if (player != null)
            {
                var myPosition = player.GetComponent<Position>();
                var futurePos = new Vector2(0);

                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    futurePos.X -= myPosition.Speed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    futurePos.X += myPosition.Speed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                {
                    futurePos.Y -= myPosition.Speed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    futurePos.Y += myPosition.Speed;
                }

                if (futurePos.X != 0 || futurePos.Y != 0)
                {
                    var futureAngle = (float)Math.Atan2(futurePos.Y, futurePos.X);

                    myPosition.X += (int)(Math.Cos(futureAngle) * myPosition.Speed);
                    myPosition.Y += (int)(Math.Sin(futureAngle) * myPosition.Speed);
                }

                if (Raylib.IsMouseButtonPressed(MOUSE_LEFT_BUTTON))
                {
                    var currentAct = player.GetComponent<Act>();
                    if (currentAct == null)
                    {
                        var action = new Act() { Action = Actions.Bark, Duration = 50 };

                        player.Components.Add(action);
                    }
                    var myRender2 = player.GetComponent<Render>();
                    Console.WriteLine($"{player.Id}[{myRender2.Texture.id}] at {myPosition.Rectangle.X}, {myPosition.Rectangle.Y}");

                }

                if (Raylib.IsMouseButtonPressed(MOUSE_RIGHT_BUTTON))
                {
                    var currentAct = player.GetComponent<Act>();
                    if (currentAct == null)
                    {
                        var action = new Act() { Action = Actions.Growl, Duration = 100 };

                        player.Components.Add(action);
                    }
                }

                var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Engine.Camera);
                var myRender = player.GetComponent<Render>();

                var offsetX = myPosition.X - mousePos.X;
                var offsetY = myPosition.Y - mousePos.Y;

                var directionInDegrees = Math.Atan2(offsetY, offsetX).ToDegrees() - 90;

                myRender.Direction = (float)directionInDegrees;
            }
        }
    }
}
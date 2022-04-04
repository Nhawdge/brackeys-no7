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
                        var action = new Act() { Action = Actions.Bark, Duration = 30, Damage = 100 / 30 };
                        player.Components.Add(action);

                        var sound = new ActiveSound { SoundToPlay = SoundData.Bark };
                        player.Components.Add(sound);

                    }
                    var myRender2 = player.GetComponent<Render>();
                }

                var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Engine.Camera);


                if (Raylib.IsMouseButtonPressed(MOUSE_RIGHT_BUTTON))
                {
                    var currentAct = player.GetComponent<Act>();
                    if (currentAct == null)
                    {
                        var action = new Act() { Action = Actions.Growl, Duration = 120, Damage = 120 / 120 };
                        player.Components.Add(action);
                        var sound = new ActiveSound { SoundToPlay = SoundData.Growl };
                        player.Components.Add(sound);
                    }
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                {
                    Console.WriteLine($"Mouse at {mousePos.X}, {mousePos.Y}");
                }
                var myRender = player.GetComponent<Render>();

                var offsetX = myPosition.X - mousePos.X;
                var offsetY = myPosition.Y - mousePos.Y;

                var directionInDegrees = Math.Atan2(offsetY, offsetX).ToDegrees();

                myRender.Direction = (float)directionInDegrees;
            }
        }
    }
}
using System.Numerics;
using JustWind.Components;
using JustWind.Entities;
using static Raylib_CsLo.Raylib;
using Raylib_CsLo;

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

            var player = allEntities.Find(x => x.HasTypes(typeof(Controllable), typeof(Position), typeof(Render)));
            if (player != null)
            {
                var myPosition = player.GetComponent<Position>();

                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    myPosition.X -= myPosition.Speed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    myPosition.X += myPosition.Speed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                {
                    myPosition.Y -= myPosition.Speed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    myPosition.Y += myPosition.Speed;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                {
                    //Bark
                }

                var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Engine.Camera);
                var myRender = player.GetComponent<Render>();

                var offsetX = myPosition.X - mousePos.X;
                var offsetY = myPosition.Y - mousePos.Y;

                var directionInDegrees = Math.Atan2(offsetY, offsetX) * 180 / Math.PI - 90;

                myRender.Direction = (float)directionInDegrees;
            }
        }
    }
}
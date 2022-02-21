using System.Numerics;
using JustWind.Components;
using JustWind.Entities;

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
            var player = allEntities.Find(x => x.HasTypes(typeof(Controllable), typeof(Position)));
            if (player != null)
            {
                if (Raylib_CsLo.Raylib.IsKeyDown(Raylib_CsLo.KeyboardKey.KEY_A))
                {
                    var myPosition = player.GetComponent<Position>();
                    myPosition.X -= myPosition.Speed;
                }
                if (Raylib_CsLo.Raylib.IsKeyDown(Raylib_CsLo.KeyboardKey.KEY_D))
                {
                    var myPosition = player.GetComponent<Position>();
                    myPosition.X += myPosition.Speed;
                }
                if (Raylib_CsLo.Raylib.IsKeyDown(Raylib_CsLo.KeyboardKey.KEY_W))
                {
                    var myPosition = player.GetComponent<Position>();
                    myPosition.Y -= myPosition.Speed;
                }
                if (Raylib_CsLo.Raylib.IsKeyDown(Raylib_CsLo.KeyboardKey.KEY_S))
                {
                    var myPosition = player.GetComponent<Position>();
                    myPosition.Y += myPosition.Speed;
                }

                if (Raylib_CsLo.Raylib.IsKeyDown(Raylib_CsLo.KeyboardKey.KEY_SPACE))
                {
                    //Bark
                }
            }
        }
    }
}
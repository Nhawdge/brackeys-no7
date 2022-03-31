using System.Numerics;
using JustWind.Components;
using JustWind.Entities;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Systems
{
    public class CameraSystem : System
    {
        public CameraSystem(Engine engine) : base(engine)
        {
        }

        public override void Load()
        {
        }

        public override void Update(List<Entity> allEntities)
        {
            var singleton = Engine.Singleton.GetComponent<Singleton>();
            if (singleton.State == GameState.Game)
            {
                var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Engine.Camera);

                var player = allEntities.FirstOrDefault(x => x.HasTypes(typeof(Controllable)));
                if (player != null)
                {
                    var playerPos = player.GetComponent<Position>();
                    var xdiff = (playerPos.X - mousePos.X) * 0.25;
                    var ydiff = (playerPos.Y - mousePos.Y) * 0.25;

                    var x = playerPos.X - xdiff;
                    var y = playerPos.Y - ydiff;

                    Engine.Camera = Engine.Camera with { 
                        target = new Vector2((float)x, (float)y), 
                        offset = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2) };
                }
            }
        }
    }
}
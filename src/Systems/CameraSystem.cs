using System.Numerics;
using JustWind.Components;
using JustWind.Entities;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Systems
{
    public class CameraSystem : System
    {
        int LeftEdge = 0 + GetScreenWidth() / 2;
        int RightEdge = 4096 - GetScreenWidth() / 2;
        int TopEdge = 0 + GetScreenHeight() / 2;
        int BottomEdge = 4096 - GetScreenHeight() / 2;

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
                int LeftEdge = 0 + GetScreenWidth() / 2;
                int RightEdge = 4096 - GetScreenWidth() / 2;
                int TopEdge = 0 + GetScreenHeight() / 2;
                int BottomEdge = 4096 - GetScreenHeight() / 2;
                var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Engine.Camera);

                var player = allEntities.FirstOrDefault(x => x.HasTypes(typeof(Controllable)));
                if (player != null)
                {
                    var playerPos = player.GetComponent<Position>();
                    var xdiff = (playerPos.X - mousePos.X) * 0.25;
                    var ydiff = (playerPos.Y - mousePos.Y) * 0.25;

                    var x = playerPos.X - xdiff;
                    if (x < LeftEdge)
                    {
                        x = LeftEdge;
                    }
                    else if (x > RightEdge)
                    {
                        x = RightEdge;
                    }
                    var y = playerPos.Y - ydiff;
                    if (y < TopEdge)
                    {
                        y = TopEdge;
                    }
                    else if (y > BottomEdge)
                    {
                        y = BottomEdge;
                    }

                    Engine.Camera = Engine.Camera with
                    {
                        target = new Vector2((float)x, (float)y),
                        offset = new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2)
                    };
                }
            }
        }
    }
}
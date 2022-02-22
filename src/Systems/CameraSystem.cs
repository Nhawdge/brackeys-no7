using JustWind.Components;
using JustWind.Entities;

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
            if (singleton.State != GameState.Game)
            {
                var player = allEntities.FirstOrDefault(x => x.HasTypes(typeof(Controllable)));
                if (player != null)
                {
                    var playerPos = player.GetComponent<Position>();
                    Engine.Camera = Engine.Camera with { target = playerPos.AsVector() };
                }
            }
        }
    }
}
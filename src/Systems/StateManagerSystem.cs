using JustWind.Components;
using JustWind.Entities;

namespace JustWind.Systems
{
    public class StateManagerSystem : System
    {
        public StateManagerSystem(Engine engine) : base(engine)
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
            

        }
    }
}
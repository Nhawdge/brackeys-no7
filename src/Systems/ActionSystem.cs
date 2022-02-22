using JustWind.Components;
using JustWind.Entities;

namespace JustWind.Systems
{
    public class ActionSystem : Systems.System
    {
        public ActionSystem(Engine engine) : base(engine)
        {
        }

        public override void Load()
        {
        }

        public override void Update(List<Entity> allEntities)
        {
            var singleton = Engine.Singleton.GetComponent<Singleton>();
 
        }
    }
}
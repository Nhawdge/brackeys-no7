using JustWind.Components;
using JustWind.Entities;
using static JustWind.Entities.EntityService;

namespace JustWind.Systems
{
    public class GenerationSystem : Systems.System
    {
        public GenerationSystem(Engine engine) : base(engine)
        {
        }

        public override void Load()
        {
            Engine.Entities.Add(CreatePlayer());
        }

        public override void Update(List<Entity> allEntities)
        {

        }
    }
}
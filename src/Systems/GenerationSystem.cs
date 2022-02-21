using ZombieEscape.Components;
using ZombieEscape.Entities;
using static ZombieEscape.Entities.EntityService;

namespace ZombieEscape.Systems
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
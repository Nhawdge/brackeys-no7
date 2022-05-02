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
            Engine.Entities.Add(CreateWaterDish());
            Engine.Entities.Add(CreateFoodDish());
            Engine.Entities.Add(CreateHouse());
            Engine.Entities.Add(CreateWestFence());
            Engine.Entities.Add(CreateNorthFence());
            Engine.Entities.Add(CreateSouthBarrier());
            Engine.Entities.Add(CreateEastBarrier());
            //Engine.Entities.Add(CreateSunTimer());

            Engine.Entities.Add(CreatePlayer());
        }

        public override void Update(List<Entity> allEntities)
        {
            var singleton = Engine.Singleton.GetComponent<Singleton>();
            if (singleton.State == GameState.Game)
            {
                var allEnemies = allEntities.Where(x => x.HasTypes(typeof(EnemyAi)));
                if (allEnemies.Count() < 4 && singleton.LastSpawnTime > 10)
                {
                    Engine.Entities.Add(CreateRandomEnemy(singleton.Stats.Round));
                    singleton.LastSpawnTime = 0;
                }
                singleton.LastSpawnTime += Raylib_CsLo.Raylib.GetFrameTime();
            }
            if (singleton.State == GameState.NextRound)
            {
                var allEnemies = allEntities.Where(x => x.HasTypes(typeof(EnemyAi))).ToList();
                foreach (var enemy in allEnemies)
                {
                    Engine.Entities.Remove(enemy);
                }
                singleton.HouseSafety = singleton.MaxHouseSafety;

                singleton.State = GameState.Game;
            }
        }
    }
}
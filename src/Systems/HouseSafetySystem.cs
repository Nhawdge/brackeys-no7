using System;
using JustWind.Components;
using JustWind.Entities;
using Raylib_CsLo;

namespace JustWind.Systems
{

    public class HouseSafetySystem : JustWind.Systems.System
    {
        public HouseSafetySystem(Engine engine) : base(engine)
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
                var time = Raylib.GetTime();
                // Enemies do -5 per second
                // Friendlies do +2 per second
                var scaryEnemies = allEntities.Where(x => x.HasTypes(typeof(EnemyAi)));
                foreach (var enemy in scaryEnemies)
                {
                    var ai = enemy.GetComponent<EnemyAi>();
                    if (ai.Scariness > 0 && ai.LastTimeDamageDealt < (time - 1))
                    {
                        ai.LastTimeDamageDealt = time;
                        singleton.HouseSafety -= 5;
                    }
                    if (ai.Scariness < 0 && ai.LastTimeDamageDealt < (time - 1))
                    {
                        ai.LastTimeDamageDealt = time;
                        singleton.HouseSafety += 4;
                    }
                }
            }
        }
    }
}
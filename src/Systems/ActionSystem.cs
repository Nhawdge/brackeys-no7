using System.Data;
using System.Reflection;
using JustWind.Components;
using JustWind.Entities;
using static JustWind.Helpers.MathHelpers;

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
            var actionables = allEntities.FindAll(x => x.HasTypes(typeof(Act)));

            foreach (var actor in actionables)
            {
                var action = actor.GetComponent<Act>();
                //Console.WriteLine("Action: " + action.Action);
                action.Duration--;
                var myPosition = actor.GetComponent<Position>();

                var nearestTarget = allEntities.Where(x => x.HasTypes(typeof(EnemyAi), typeof(Position)))
                    .OrderBy(x => DistanceBetween(x.GetComponent<Position>().AsVector(), myPosition.AsVector())).FirstOrDefault();

                if (nearestTarget != null)
                {
                    var targetPosition = nearestTarget.GetComponent<Position>();
                    if (DistanceBetween(targetPosition.AsVector(), myPosition.AsVector()) < 250)
                    {
                        var targetAi = nearestTarget.GetComponent<EnemyAi>();
                        targetAi.Scariness -= 1;
                        Console.WriteLine("Reducing scariness!");
                    }
                }

                if (action.Duration <= 0)
                {
                    actor.Components.Remove(action);
                }
            }
        }
    }
}
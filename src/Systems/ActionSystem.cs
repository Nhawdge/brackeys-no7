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
                action.Duration--;
                var myPosition = actor.GetComponent<Position>();

                if (action.Action == Actions.Growl)
                {
                    var nearestTarget = allEntities.Where(x => x.HasTypes(typeof(EnemyAi), typeof(Position)))
                        .OrderBy(x => DistanceBetween(x.GetComponent<Position>().AsVector(), myPosition.AsVector())).FirstOrDefault();

                    if (nearestTarget != null)
                    {
                        var targetPosition = nearestTarget.GetComponent<Position>();
                        var targetAi = nearestTarget.GetComponent<EnemyAi>();
                        var distancebetween = DistanceBetween(targetPosition.AsVector(), myPosition.AsVector());

                        //   0 - 250 = 100%
                        // 250 - 500 = 75%
                        // 500 - 750 = 50%
                        if (distancebetween < 200)
                        {
                            targetAi.Scariness -= action.Damage;
                            Console.WriteLine("Full damage");
                        }
                        else if (distancebetween < 400)
                        {
                            targetAi.Scariness -= (int)(action.Damage * .75f);
                            Console.WriteLine("75% damage");
                        }
                        else if (distancebetween < 500)
                        {
                            targetAi.Scariness -= (int)(action.Damage * .5f);
                            Console.WriteLine("50% damage");
                        }
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
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
            var actionables = allEntities.FindAll(x => x.HasTypes(typeof(Act)));

            foreach (var actor in actionables)
            {
                var action = actor.GetComponent<Act>();
                Console.WriteLine("Action: " + action.Action);
                action.Duration--;

                if (action.Duration <= 0)
                {
                    actor.Components.Remove(action);
                }
            }
        }
    }
}
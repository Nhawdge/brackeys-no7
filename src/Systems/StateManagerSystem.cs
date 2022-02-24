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

            var actionables = allEntities.FindAll(x => x.HasTypes(typeof(State)));
            foreach (var actor in actionables)
            {
                var myAct = actor.GetComponent<Act>();
                var myState = actor.GetComponent<State>();
                if (myAct != null)
                {
                    switch (myAct.Action)
                    {
                        case Actions.Bark:
                            myState.CurrentState = CharacterState.Bark;
                            break;
                        case Actions.Growl:
                            myState.CurrentState = CharacterState.Growl;
                            break;
                    }
                }
                else
                {
                    myState.CurrentState = CharacterState.Idle;
                }
            }
        }
    }
}
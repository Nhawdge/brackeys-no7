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
                var currentAnimation = actor.GetComponent<Animation>();
                CharacterState newState;
                if (myAct != null)
                {
                    switch (myAct.Action)
                    {
                        case Actions.Bark:
                            newState = CharacterState.Bark;
                            break;
                        case Actions.Growl:
                            newState = CharacterState.Growl;
                            break;
                        default:
                            newState = CharacterState.Idle;
                            break;
                    }
                }
                else
                {
                    newState = CharacterState.Idle;
                }

                if (myState.CurrentState != newState)
                {
                    Console.WriteLine($"{actor.Id} is {newState}, was {myState.CurrentState}");
                    switch (newState)
                    {
                        case CharacterState.Bark:
                            currentAnimation.Animations = AnimationData.DogBark;
                            break;
                        case CharacterState.Growl:
                            currentAnimation.Animations = AnimationData.DogGrowl;
                            break;
                        case CharacterState.Idle:
                        default:
                            currentAnimation.Animations = AnimationData.DogWag;
                            break;
                    }
                    myState.CurrentState = newState;
                }
            }
        }
    }
}
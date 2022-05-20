using System;
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
            var rand = new Random();

            foreach (var enemy in allEntities.FindAll(x => x.HasTypes(typeof(EnemyAi))))
            {
                var myAi = enemy.GetComponent<EnemyAi>();
                var currentState = myAi.EnemyState;
                var newState = myAi.EnemyState;

                if (myAi.EnemyState == EnemyStates.Peaceful)
                {
                    continue;
                }

                if (myAi.Scariness > 0)
                {
                    newState = EnemyStates.Evil;
                }
                else if (myAi.Scariness <= 0 && myAi.EnemyState == EnemyStates.Evil)
                {
                    var myAnimation = enemy.GetComponent<Animation>();
                    myAnimation.Animations = AnimationData.Transition;
                    myAi.EnemyState = EnemyStates.Transition;
                    myAi.LastTimeDamageDealt = 0;
                }
                if (myAi.EnemyState == EnemyStates.Transition)
                {
                    myAi.LastTimeDamageDealt += Raylib_CsLo.Raylib.GetFrameTime();
                    if (myAi.LastTimeDamageDealt > 1)
                    {
                        myAi.EnemyState = EnemyStates.Peaceful;
                        var myAnimation = enemy.GetComponent<Animation>();

                        myAnimation.Animations = AnimationData.EnemyOptions.ElementAt(rand.Next(0, AnimationData.EnemyOptions.Count));

                    }
                }

            }
        }
    }
}
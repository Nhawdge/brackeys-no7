using System.Numerics;
using JustWind.Components;
using JustWind.Entities;
using static JustWind.Helpers.MathHelpers;

namespace JustWind.Systems
{
    public class AiMovementSystem : Systems.System
    {
        public AiMovementSystem(Engine engine) : base(engine)
        {
        }

        public override void Load() { }

        public override void Update(List<Entity> allEntities)
        {
            var rand = new Random();
            var singleton = Engine.Singleton.GetComponent<Singleton>();
            if (singleton.State == GameState.Game)
            {
                var entitiesToRemove = new List<Entity>();
                foreach (var entity in allEntities.Where(entity => entity.HasTypes(typeof(Position), typeof(EnemyAi), typeof(Animation))))
                {
                    var myPosition = entity.GetComponent<Position>();
                    var myAi = entity.GetComponent<EnemyAi>();
                    if (myAi.Scariness == 0)
                    {
                        myAi.Scariness = -1;
                        var myAnimation = entity.GetComponent<Animation>();
                        myAnimation.Animations = AnimationData.EnemyOptions.ElementAt(rand.Next(0, AnimationData.EnemyOptions.Count));
                    }
                    if (myAi.NextTarget == new Vector2(0) && myAi.Path != null)
                    {
                        myAi.NextTarget = myAi.Path[myAi.NextIndex];
                    }
                    if (DistanceBetween(myPosition.AsVector(), myAi.NextTarget) < myPosition.Speed)
                    {
                        myAi.NextIndex++;
                        if (myAi.NextIndex >= myAi.Path.Count)
                        {
                            entitiesToRemove.Add(entity);
                            continue;
                        }
                        myAi.NextTarget = myAi.Path[myAi.NextIndex];
                    }

                    var angle = (float)Math.Atan2(myAi.NextTarget.Y - myPosition.Y, myAi.NextTarget.X - myPosition.X);
                    var myRender = entity.GetComponent<Render>();
                    myRender.Direction = angle.ToDegrees();
                    myPosition.X += (int)(Math.Cos(angle) * myAi.Speed);
                    myPosition.Y += (int)(Math.Sin(angle) * myAi.Speed);
                }
                foreach (var removed in entitiesToRemove)
                {
                    allEntities.Remove(removed);
                }
            }
        }

    }
}
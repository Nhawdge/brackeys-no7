using System.Numerics;
using System;
using System.Collections.Generic;
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
            var singleton = Engine.Singleton.GetComponent<Singleton>();
            if (singleton.State == GameState.Game)
            {

                foreach (var entity in allEntities)
                {
                    if (!entity.HasTypes(typeof(Position), typeof(EnemyAi)))
                    {
                        continue;
                    }
                    var myPosition = entity.GetComponent<Position>();
                    var myAi = entity.GetComponent<EnemyAi>();

                    if (myAi.NextTarget == new Vector2(0) && myAi.Path != null)
                    {
                        myAi.NextTarget = myAi.Path[myAi.NextIndex];
                    }
                    if (DistanceBetween(myPosition.AsVector(), myAi.NextTarget) < myPosition.Speed)
                    {
                        myAi.NextIndex++;
                        if (myAi.NextIndex >= myAi.Path.Count)
                        {
                            myAi.NextIndex = 0;
                        }
                        myAi.NextTarget = myAi.Path[myAi.NextIndex];
                    }

                    var angle = (float)Math.Atan2(myAi.NextTarget.Y - myPosition.Y, myAi.NextTarget.X - myPosition.X);
                    var myRender = entity.GetComponent<Render>();
                    myRender.Direction = angle.ToDegrees();
                    myPosition.X += (int)(Math.Cos(angle) * myAi.Speed);
                    myPosition.Y += (int)(Math.Sin(angle) * myAi.Speed);
                    //Console.WriteLine($"{myPosition.X}, {myPosition.Y},{angle} {myAi.NextTarget.X}, {myAi.NextTarget.Y}");
                    //Console.WriteLine($"Path: {myAi.Path[0]}, {myAi.NextIndex}, target: {myAi.NextTarget.X}, {myAi.NextTarget.Y}");
                }
            }
        }

    }
}
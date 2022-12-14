using System.Data;
using System.Numerics;
using JustWind.Components;
using JustWind.Entities;
using Raylib_CsLo;
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
            var time = Raylib.GetFrameTime();
            var singleton = Engine.Singleton.GetComponent<Singleton>();
            var actionables = allEntities.FindAll(x => x.HasTypes(typeof(Act), typeof(Position)));

            foreach (var actor in actionables)
            {
                var action = actor.GetComponent<Act>();
                var myPosition = actor.GetComponent<Position>();
                var myRender = actor.GetComponent<Render>();

                action.ActionTimer += time;
                if (action.Action == Actions.Bark)
                {
                    var leftDegrees = (myRender.Direction + 180 - 25) % 360;
                    var rightDegrees = (myRender.Direction + 180 + 25) % 360;
                    var distance = 750;
                    var leftX = myPosition.X + (Math.Cos(leftDegrees.ToRadians()) * distance);
                    var leftY = myPosition.Y + (Math.Sin(leftDegrees.ToRadians()) * distance);
                    var rightX = myPosition.X + Math.Cos(rightDegrees.ToRadians()) * distance;
                    var rightY = myPosition.Y + Math.Sin(rightDegrees.ToRadians()) * distance;
                    var leftCorner = new Vector2((int)leftX, (int)leftY);
                    var rightCorner = new Vector2((int)rightX, (int)rightY);
#if DEBUG
                    Raylib.DrawLineV(leftCorner, rightCorner, Raylib.BLACK);
                    Raylib.DrawLineV(myPosition.AsVector(), rightCorner, Raylib.BLACK);
                    Raylib.DrawLineV(myPosition.AsVector(), leftCorner, Raylib.BLACK);
#endif
                    if (action.ActionTimer > action.CooldownInSeconds / action.TotalDamageTicks)
                    {
                        var targets = allEntities.Where(x => x.HasTypes(typeof(EnemyAi), typeof(Position)));
                        action.TotalDamageTicks--;


                        var nearestTargets = allEntities
                            .Where(x => x.HasTypes(typeof(EnemyAi), typeof(Position)))
                            .OrderBy(x => DistanceBetween(x.GetComponent<Position>().AsVector(), myPosition.AsVector()))
                            .Where(x => x.GetComponent<EnemyAi>().Scariness > 0);

                        foreach (var target in nearestTargets)
                        {
                            var targetPosition = target.GetComponent<Position>();

                            var inTriangle = Raylib.CheckCollisionPointTriangle(targetPosition.AsVector(), leftCorner, rightCorner, myPosition.AsVector());
                            if (inTriangle)
                            {
                                var targetAi = target.GetComponent<EnemyAi>();

                                var distancebetween = DistanceBetween(targetPosition.AsVector(), myPosition.AsVector());
                                var damage = 0f;

                                if (distancebetween < 250)
                                {
                                    damage = action.DamagePerTick;
                                }
                                else if (distancebetween < 400)
                                {
                                    damage = (int)(action.DamagePerTick * .75f);
                                }
                                else if (distancebetween < 500)
                                {
                                    damage = (int)(action.DamagePerTick * .5f);
                                }
                                var debuff = target.GetComponent<Debuff<DamageAmplify>>();
                                if (debuff != null && debuff.DebuffType.ActionToAmplify == Actions.Bark)
                                {
                                    damage *= debuff.Value;
                                }
                                targetAi.Scariness -= Math.Min(targetAi.Scariness, damage);
                            }
                        }
                    }
                }

                if (action.Action == Actions.Growl)
                {
                    var nearestTargets = allEntities
                        .Where(x => x.HasTypes(typeof(EnemyAi), typeof(Position)))
                        .OrderBy(x => DistanceBetween(x.GetComponent<Position>().AsVector(), myPosition.AsVector()))
                        .Where(x => x.GetComponent<EnemyAi>().Scariness > 0);
#if DEBUG
                    Raylib.DrawCircleLines((int)myPosition.X, (int)myPosition.Y, 400, Raylib.GREEN);
                    Raylib.DrawCircleLines((int)myPosition.X, (int)myPosition.Y, 750, Raylib.YELLOW);
#endif
                    if (action.ActionTimer > action.CooldownInSeconds / action.TotalDamageTicks)
                    {
                        action.TotalDamageTicks--;
                        foreach (var nearestTarget in nearestTargets)
                        {
                            var targetPosition = nearestTarget.GetComponent<Position>();
                            var targetAi = nearestTarget.GetComponent<EnemyAi>();
                            var distancebetween = DistanceBetween(targetPosition.AsVector(), myPosition.AsVector());
                            var damage = 0;

                            if (distancebetween < 400)
                            {
                                damage = action.DamagePerTick;
                                var targetDebuff = nearestTarget.GetComponent<Debuff<DamageAmplify>>();
                                if (targetDebuff == null)
                                {
                                    targetDebuff = new Debuff<DamageAmplify>
                                    {
                                        DebuffType = new DamageAmplify { ActionToAmplify = Actions.Bark }
                                    };
                                    nearestTarget.Components.Add(targetDebuff);
                                }
                                targetDebuff.Value += .2f;
                                targetDebuff.Intensity++;
                            }
                            else if (distancebetween < 750)
                            {
                                damage = (int)(action.DamagePerTick * .66f);
                            }                         
                            targetAi.Scariness -= Math.Min(targetAi.Scariness, damage);
                        }
                    }
                }

                if (action.TotalDamageTicks <= 0)
                {
                    actor.Components.Remove(action);
                }
            }
        }
    }
}
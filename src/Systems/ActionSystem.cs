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
                    var leftDegrees = (myRender.Direction + 180 - 15) % 360;
                    var rightDegrees = (myRender.Direction + 180 + 15) % 360;
                    var leftX = myPosition.X + (Math.Cos(leftDegrees.ToRadians()) * 500);
                    var leftY = myPosition.Y + (Math.Sin(leftDegrees.ToRadians()) * 500);
                    var rightX = myPosition.X + Math.Cos(rightDegrees.ToRadians()) * 500;
                    var rightY = myPosition.Y + Math.Sin(rightDegrees.ToRadians()) * 500;
                    var leftCorner = new Vector2((int)leftX, (int)leftY);
                    var rightCorner = new Vector2((int)rightX, (int)rightY);

                    Raylib.DrawLineV(leftCorner, rightCorner, Raylib.BLACK);
                    Raylib.DrawLineV(myPosition.AsVector(), rightCorner, Raylib.BLACK);
                    Raylib.DrawLineV(myPosition.AsVector(), leftCorner, Raylib.BLACK);

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
                                var debuff = target.GetComponent<Debuff>();
                                if (debuff != null)
                                {
                                    damage *= debuff.Amount;
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

                    Raylib.DrawCircleLines((int)myPosition.X, (int)myPosition.Y, 200, Raylib.GREEN);
                    Raylib.DrawCircleLines((int)myPosition.X, (int)myPosition.Y, 400, Raylib.YELLOW);
                    Raylib.DrawCircleLines((int)myPosition.X, (int)myPosition.Y, 750, Raylib.RED);
                    if (action.ActionTimer > action.CooldownInSeconds / action.TotalDamageTicks)
                    {
                        action.TotalDamageTicks--;
                        foreach (var nearestTarget in nearestTargets)
                        {
                            var targetPosition = nearestTarget.GetComponent<Position>();
                            var targetAi = nearestTarget.GetComponent<EnemyAi>();
                            var distancebetween = DistanceBetween(targetPosition.AsVector(), myPosition.AsVector());
                            var damage = 0;

                            if (distancebetween < 200)
                            {
                                damage = action.DamagePerTick;
                                var targetDebuff = nearestTarget.GetComponent<Debuff>();
                                if (targetDebuff == null)
                                {
                                    targetDebuff = new Debuff() { Type = Debuffs.AmplifyDamage };
                                    nearestTarget.Components.Add(targetDebuff);
                                }
                                targetDebuff.Amount += .2f;
                            }
                            else if (distancebetween < 400)
                            {
                                damage = (int)(action.DamagePerTick * .66f);
                            }
                            else if (distancebetween < 750)
                            {
                                damage = (int)(action.DamagePerTick * .33f);
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
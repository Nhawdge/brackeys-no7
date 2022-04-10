using System.Numerics;
using JustWind.Components;
using JustWind.Entities;
using static Raylib_CsLo.Raylib;
using Raylib_CsLo;
using JustWind.Helpers;

namespace JustWind.Systems
{
    public class ControllableSystem : Systems.System
    {
        public ControllableSystem(Engine engine) : base(engine)
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

            var player = allEntities.Find(x => x.HasTypes(typeof(Controllable), typeof(Position), typeof(Render)));
            if (player != null)
            {
                var myPosition = player.GetComponent<Position>();
                var speed = myPosition.Speed * 30 * Raylib.GetFrameTime();
                var futurePos = new Vector2(0);

                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    futurePos.X -= speed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    futurePos.X += speed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                {
                    futurePos.Y -= speed;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    futurePos.Y += speed;
                }

                if (futurePos.X != 0 || futurePos.Y != 0)
                {
                    var futureAngle = (float)Math.Atan2(futurePos.Y, futurePos.X);

                    // Collision Check
                    var predictedPos = new Vector2(myPosition.X + (float)(Math.Cos(futureAngle) * speed), myPosition.Y + (float)(Math.Sin(futureAngle) * speed));
                    var myCollision = player.GetComponent<Collision<CircleBoundType>>();
                    var willCollide = false;
                    foreach (var collidable in allEntities.Where(x => x.HasTypes(typeof(Collision<RectangleBoundType>))))
                    {
                        var nearestPoint = new Vector2();
                        var targetRect = collidable.GetComponent<Collision<RectangleBoundType>>();
                        if (targetRect != null)
                        {
                            var xMiddle = false;
                            var yMiddle = false;
                            var tarRect = targetRect.BoundType.Rectangle;
                            // check which X edge
                            if (predictedPos.X < tarRect.X)
                            {
                                nearestPoint.X = tarRect.X;
                            }
                            else if (predictedPos.X > tarRect.X + tarRect.width)
                            {
                                nearestPoint.X = tarRect.X + tarRect.width;
                            }
                            else
                            {
                                nearestPoint.X = predictedPos.X;
                                xMiddle = true;
                            }

                            if (predictedPos.Y < tarRect.y)
                            {
                                nearestPoint.Y = tarRect.Y;
                            }
                            else if (predictedPos.Y > tarRect.Y + tarRect.height)
                            {
                                nearestPoint.Y = tarRect.Y + tarRect.height;
                            }
                            else
                            {
                                nearestPoint.Y = predictedPos.Y;
                                yMiddle = true;
                            }

                            var inside = xMiddle && yMiddle;
                            // if (inside)
                            // {
                            //     var nearestEdge = new Vector2();
                            //     if (predictedPos.X < tarRect.X)
                            //     {
                            //         nearestEdge.X = tarRect.X;
                            //     }
                            //     else if (predictedPos.X > tarRect.X + tarRect.width)
                            //     {
                            //         nearestEdge.X = tarRect.y;
                            //     }

                            //     if (predictedPos.Y < tarRect.y)
                            //     {
                            //         nearestEdge.Y = tarRect.y;
                            //     }
                            //     else if (predictedPos.Y > tarRect.Y + tarRect.height)
                            //     {
                            //         nearestEdge.Y = tarRect.Y;
                            //     }
                            // }
                            DrawCircleV(nearestPoint, 5, BLACK);
                            if (CheckCollisionPointCircle(nearestPoint, predictedPos, myCollision.BoundType.Radius))
                            {
                                willCollide = true;
                            }

                        }
                    }
                    if (!willCollide)
                    {
                        myPosition.X += (float)(Math.Cos(futureAngle) * speed);
                        myPosition.Y += (float)(Math.Sin(futureAngle) * speed);
                    }

                }

                var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Engine.Camera);
                if (Raylib.IsMouseButtonPressed(MOUSE_LEFT_BUTTON))
                {
                    var currentAct = player.GetComponent<Act>();
                    if (currentAct == null)
                    {
                        var action = new Act(Actions.Bark);
                        player.Components.Add(action);

                        var sound = new ActiveSound { SoundToPlay = SoundData.Bark };
                        player.Components.Add(sound);

                    }
                    var myRender2 = player.GetComponent<Render>();
                }

                if (Raylib.IsMouseButtonPressed(MOUSE_RIGHT_BUTTON))
                {
                    var currentAct = player.GetComponent<Act>();
                    if (currentAct == null)
                    {
                        var action = new Act(Actions.Growl);
                        player.Components.Add(action);
                        var sound = new ActiveSound { SoundToPlay = SoundData.Growl };
                        player.Components.Add(sound);
                    }
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                {
                    Console.WriteLine($"Mouse at {mousePos.X}, {mousePos.Y}");
                }

                var myRender = player.GetComponent<Render>();

                var offsetX = myPosition.X - mousePos.X;
                var offsetY = myPosition.Y - mousePos.Y;

                var directionInDegrees = Math.Atan2(offsetY, offsetX).ToDegrees();

                myRender.Direction = (float)directionInDegrees;
            }
        }
    }
}
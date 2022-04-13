using JustWind.Entities;
using System.Numerics;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;
using JustWind.Components;

namespace JustWind.Systems
{
    public class RenderingSystem : System
    {
        public RenderingSystem(Engine engine) : base(engine)
        {
        }

        public Texture backgroundTexture;

        public override void Load()
        {
            backgroundTexture = LoadTexture("src/Assets/scene/scene_4096x4096.png");
        }

        public override void Update(List<Entity> allEntities)
        {
            var singleton = Engine.Singleton.GetComponent<Singleton>();


            if (singleton.State == GameState.Game)
            {
                var bgSourceRect = new Rectangle(0, 0, backgroundTexture.width, backgroundTexture.height);
                var bgDestinationRect = new Rectangle(0, 0, backgroundTexture.width, backgroundTexture.height);
                DrawTexturePro(backgroundTexture, bgSourceRect, bgDestinationRect, new Vector2(0), 0f, Raylib.WHITE);

                foreach (var entity in allEntities)
                {
                    if (entity.HasTypes(typeof(Render), typeof(Position)))
                    {
                        var myRender = entity.GetComponent<Render>();
                        var myPosition = entity.GetComponent<Position>();

                        DrawTexturePro(myRender.Texture, myRender.Rectangle, myPosition.Rectangle, myPosition.GetRectCenter(), myRender.Direction - 90, Raylib.WHITE);
                       
                        if (entity.HasTypes(typeof(Collision<CircleBoundType>)))
                        {
                            var collision = entity.GetComponent<Collision<CircleBoundType>>();
                            DrawCircleLines((int)myPosition.Rectangle.X, (int)myPosition.Rectangle.Y, collision.BoundType.Radius, Raylib.GREEN);
                        }
                        if (entity.HasTypes(typeof(Collision<RectangleBoundType>)))
                        {
                            var collision = entity.GetComponent<Collision<RectangleBoundType>>();
                            //DrawRectangleRec(collision.BoundType.Rectangle, Raylib.RED);
                        }
                        if (entity.HasTypes(typeof(EnemyAi)))
                        {
                            var myAi = entity.GetComponent<EnemyAi>();
                            if (myAi.Scariness > 0)
                            {
                                var percent = ((float)myAi.Scariness / (float)myAi.MaxScariness);
                                var textToDraw = (percent * 100).ToString("0.0");
                                var width = percent * 100;

                                DrawRectangle((int)myPosition.X - 54, (int)myPosition.Y - (int)(myPosition.Rectangle.height / 2), 108, 20, Raylib.BLACK);
                                DrawRectangle((int)myPosition.X - 50, (int)myPosition.Y - (int)(myPosition.Rectangle.height / 2) + 5, (int)width, 10, Raylib.RED);

                            }
                            //DrawRectangle(GetScreenWidth() / 2 - 100, 10, (int)width, 20, Raylib.RED);
                            //DrawText($"{textToDraw}", (GetScreenWidth() / 2) - (MeasureText(textToDraw, 12) / 2), 10, 20, Raylib.WHITE);
                        }
                    
                    }
                }
            }
        }
    }
}
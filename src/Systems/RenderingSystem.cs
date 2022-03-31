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
        public Texture MenuTexture;

        public override void Load()
        {
            backgroundTexture = LoadTexture("src/Assets/scene/scene_4096x4096.png");
            MenuTexture = LoadTexture("src/Assets/scene/cover.png");
        }

        public override void Update(List<Entity> allEntities)
        {
            var singleton = Engine.Singleton.GetComponent<Singleton>();

            if (singleton.State == GameState.Menu)
            {
                var bgMenuSourceRect = new Rectangle(0, 0, MenuTexture.width, MenuTexture.height);
                //Console.WriteLine(Raylib.GetScreenWidth());
                var bgMenuDestinationRect = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
                DrawTexturePro(MenuTexture, bgMenuSourceRect, bgMenuDestinationRect, new Vector2(0), 0f, Raylib.WHITE);
                return;
            }

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
                        //DrawText($"{entity.ShortId()}", myPosition.Rectangle.X, myPosition.Rectangle.Y, 20, Raylib.BLACK);
                        //DrawCircle((int)myPosition.Rectangle.X, (int)myPosition.Rectangle.Y, 500f, Raylib.GREEN);
                        //DrawCircle((int)myPosition.Rectangle.X, (int)myPosition.Rectangle.Y, 400f, Raylib.YELLOW);
                        //DrawCircle((int)myPosition.Rectangle.X, (int)myPosition.Rectangle.Y, 200f, Raylib.RED);
                    }
                }
            }
        }
    }
}
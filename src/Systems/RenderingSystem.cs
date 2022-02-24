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

        public Texture backgroundTexture { get; set; }
        public Texture MenuTexture { get; set; }

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
                var bgMenuSourceRect = new Rectangle(0, 0, backgroundTexture.width, backgroundTexture.height);
                var bgMenuDestinationRect = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
                //Console.WriteLine($"{bgDestinationRect.width}, {bgDestinationRect.height}");
                DrawTexturePro(MenuTexture, bgMenuSourceRect, bgMenuDestinationRect, new Vector2(0), 0f, Raylib.WHITE);
                return;
            }

            if (singleton.State != GameState.Game)
            {
                return;
            }

            var bgSourceRect = new Rectangle(0, 0, backgroundTexture.width, backgroundTexture.height);
            var bgDestinationRect = new Rectangle(0, 0, backgroundTexture.width, backgroundTexture.height);
            //Console.WriteLine($"{bgDestinationRect.width}, {bgDestinationRect.height}");
            DrawTexturePro(backgroundTexture, bgSourceRect, bgDestinationRect, new Vector2(0), 0f, Raylib.WHITE);

            foreach (var entity in allEntities)
            {
                if (entity.HasTypes(typeof(Render), typeof(Position)))
                {
                    var myRender = entity.GetComponent<Render>();
                    var myPosition = entity.GetComponent<Position>();

                    DrawTexturePro(myRender.Texture, myRender.Rectangle, myPosition.Rectangle, myPosition.GetRectCenter(), myRender.Direction, Raylib.WHITE);
                    //DrawCircle((int)myPosition.X, (int)myPosition.Y, 5f, Raylib.BLACK);
                }
            }
        }
    }
}
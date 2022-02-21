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

        public override void Load()
        {
            backgroundTexture = LoadTexture("src/Assets/background.png");
        }

        public override void Update(List<Entity> allEntities)
        {
            var bgSourceRect = new Rectangle(0, 0, Raylib.GetScreenWidth(), backgroundTexture.height);
            var bgDestinationRect = new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            //Console.WriteLine($"{bgDestinationRect.width}, {bgDestinationRect.height}");
            DrawTexturePro(backgroundTexture, bgSourceRect, bgDestinationRect, new Vector2(0), 0f, Raylib.WHITE);
            foreach (var entity in allEntities)
            {
                if (entity.HasTypes(typeof(Render), typeof(Position)))
                {
                    var myRender = entity.GetComponent<Render>();
                    var myPosition = entity.GetComponent<Position>();
                    DrawTexturePro(myRender.Texture, myRender.Rectangle, myPosition.Rectangle, new Vector2(0), 0f, Raylib.WHITE);
                }
            }
        }
    }
}
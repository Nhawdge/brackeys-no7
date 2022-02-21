using ZombieEscape.Entities;
using System.Numerics;
using Raylib_CsLo;

namespace ZombieEscape.Systems
{
    public class RenderingSystem : System
    {
        public RenderingSystem(Engine engine) : base(engine)
        {
        }

        public Texture backgroundTexture { get; set; }

        public override void Load()
        {

            //backgroundTexture = LoadTexture("src/Assets/.png");
        }

        public override void Update(List<Entity> allEntities)
        {
            var bgSourceRect = new Rectangle(0, 0, backgroundTexture.width, backgroundTexture.height);
            var bgDestinationRect = new Rectangle(0, 0, 800, 800);
            Raylib.DrawTexturePro(backgroundTexture, bgSourceRect, bgDestinationRect, new Vector2(0), 0f, Raylib.WHITE);
            foreach (var entity in allEntities)
            {

            }
        }
        private static bool CanRender(Entity entity)
        {
            return true;
        }
    }

}
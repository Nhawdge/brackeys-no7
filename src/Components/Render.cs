using System.Numerics;
using Raylib_CsLo;

namespace JustWind.Components
{
    public class Render : Component
    {
        public Texture Texture;
        public Rectangle Rectangle;
        public bool IsAnimated;
        public float Direction = 0f;
        public Render(Texture texture)
        {
            Texture = texture;
            Rectangle = new Rectangle(0, 0, Texture.width, Texture.height);
        }
        public Render(string path)
        {
            Texture = Raylib.LoadTexture(path);
            Rectangle = new Rectangle(0, 0, Texture.width, Texture.height);
        }
        public Vector2 GetCenter()
        {
            return new Vector2((Rectangle.width / 2), (Rectangle.height / 2));
        }
    }
}
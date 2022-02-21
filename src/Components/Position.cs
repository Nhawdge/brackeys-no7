using System.Numerics;
using Raylib_CsLo;

namespace ZombieEscape.Components
{
    public class Position : Component
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Speed { get; set; } = 2;
        public int Direction { get; set; } = 0;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }

        public Vector2 AsVector() => new Vector2(X, Y);
        public Vector2 GetCenter()
        {
            return new Vector2(X + (Width / 2), Y + (Height / 2));
        }

    }
}
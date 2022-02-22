using System.Numerics;
using Raylib_CsLo;

namespace JustWind.Components
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
            get => new Rectangle(X, Y, Width, Height);
        }

        public Rectangle RectCenter
        {
            get => new Rectangle(X - Width / 2, Y - Height / 2, Width, Height);
        }

        public Vector2 AsVector() => new Vector2(X, Y);
        
        public Vector2 GetCenter()
        {
            return new Vector2(X + (Width / 2), Y + (Height / 2));
        }
        public Vector2 GetRectCenter()
        {
            return new Vector2(Width / 2, Height / 2);
        }
    }
}
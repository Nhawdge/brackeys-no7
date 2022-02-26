using System.Numerics;
using Raylib_CsLo;

namespace JustWind.Components
{
    public class Position : Component
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int Speed = 2;
        public int Direction = 0;

        public Rectangle Rectangle => new Rectangle(X, Y, Width, Height);

        public Rectangle RectCenter => new Rectangle(X - Width / 2, Y - Height / 2, Width, Height);

        public Vector2 AsVector() => new Vector2(X, Y);

        public Vector2 GetCenter() => new Vector2(X + (Width / 2), Y + (Height / 2));

        public Vector2 GetRectCenter() => new Vector2(Width / 2, Height / 2);

    }
}
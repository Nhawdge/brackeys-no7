using System.Numerics;
namespace JustWind.Helpers
{
    public static class MathHelpers
    {
        public static int ToDegrees(this double radians)
        {
            return (int)(radians * (180 / Math.PI));
        }
        public static int ToRadians(this double degrees)
        {
            return (int)(degrees * (Math.PI / 180));
        }
        public static int ToDegrees(this float radians)
        {
            return (int)(radians * (180 / Math.PI));
        }
        public static int ToRadians(this float degrees)
        {
            return (int)(degrees * (Math.PI / 180));
        }
        public static int DistanceBetween(Vector2 a, Vector2 b)
        {
            return (int)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
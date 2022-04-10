using System.Numerics;
namespace JustWind.Helpers
{
    public static class MathHelpers
    {
        public static float ToDegrees(this double radians)
        {
            return (float)(radians * (180 / Math.PI));
        }
        public static float ToRadians(this double degrees)
        {
            return (float)(degrees * (Math.PI / 180));
        }
        public static float ToDegrees(this float radians)
        {
            return (float)(radians * (180 / Math.PI));
        }
        public static float ToRadians(this float degrees)
        {
            return (float)(degrees * (Math.PI / 180));
        }
        public static float DistanceBetween(Vector2 a, Vector2 b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
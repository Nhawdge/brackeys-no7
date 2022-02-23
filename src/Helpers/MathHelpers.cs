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
    }
}
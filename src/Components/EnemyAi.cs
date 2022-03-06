using System.Numerics;

namespace JustWind.Components
{
    public class EnemyAi : Component
    {
        public List<Vector2> Path = new();
        public int Speed = 5;
        public Vector2 NextTarget;
        public int NextIndex = 0;
        public bool PathLoops = true;

        public int Scariness = 100;

        public EnemyAi()
        {
            Path = PathData.SideWalk;
        }

        public EnemyAi(List<Vector2> path)
        {
            Path = path;
        }
    }

    public static class PathData
    {
        public static List<Vector2> SideWalk => new List<Vector2>() {
            new Vector2(600, 4100), // Bottom left
            new Vector2(600, 375), // Top left
            new Vector2(4060, 375), // Top right
        };
    }
}
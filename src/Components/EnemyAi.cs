using System.Numerics;

namespace JustWind.Components
{
    public class EnemyAi : Component
    {
        public List<Vector2> Path = new();
        public int Speed = 5;
        public int Level = 1;
        public Vector2 NextTarget;
        public int NextIndex = 0;
        public bool PathLoops = true;
        public float Scariness = 250;
        public float MaxScariness = 250;

        public double LastTimeDamageDealt = 0;
        public int Damage = 3;

        public EnemyAi()
        {
            Path = PathData.SideWalk;
        }

        public EnemyAi(List<Vector2> path, int level)
        {
            Path = path;
            Level = level;
            Scariness = 250 + ((level - 1) * 50);
            MaxScariness = Scariness;
            Damage = 2 + level;
        }
    }

    public static class PathData
    {
        public static List<Vector2> SideWalk;
        public static List<Vector2> SideWalkReverse;
        static PathData()
        {
            SideWalk = new List<Vector2>() {
                new Vector2(600, 4100), // Bottom left
                new Vector2(600, 375), // Top left
                new Vector2(4060, 375), // Top right
            };
            SideWalkReverse = new List<Vector2>() {
                new Vector2(4060, 375), // Top right
                new Vector2(600, 375), // Top left
                new Vector2(600, 4100), // Bottom left
            };
        }

        public static List<Vector2> GetRandomPath()
        {
            var rand = new Random();
            var listOfPaths = new List<List<Vector2>>() {
                SideWalk,
                SideWalkReverse,
            };

            return listOfPaths.ElementAt(rand.Next(0, listOfPaths.Count));
        }
    }
}
using System.Collections.Generic;
using JustWind.Components;

namespace JustWind.Entities
{
    public static class EntityService
    {

        public static Entity CreatePlayer()
        {
            var entity = new Entity();
            var rand = new Random();

            entity.Components.Add(new Position { X = 1000, Y = 1000, Speed = 15, Width = 256, Height = 256 });
            entity.Components.Add(new Render("src/Assets/dog/dog_wag1.png"));
            entity.Components.Add(new Animation(AnimationData.DogWag));
            entity.Components.Add(new Controllable());
            entity.Components.Add(new State());
            entity.Components.Add(new Collision() { CollisionState = CollisionStates.Dynamic });

            return entity;
        }

        public static Entity CreateRandomEnemy()
        {
            var entity = new Entity();
            var rand = new Random();

            //entity.Components.Add(new Position { X = Raylib_CsLo.Raylib.GetScreenWidth() / 2, Y = Raylib_CsLo.Raylib.GetScreenHeight() / 2, Speed = 5, Width = 256, Height = 256 });
            var level = rand.Next(1, 4);
            var animationSet = level switch
            {
                1 => AnimationData.ScaryOptionsL1,
                2 => AnimationData.ScaryOptionsL2,
                3 => AnimationData.ScaryOptionsL3,
                _ => AnimationData.ScaryOptionsL1,
            };

            entity.Components.Add(new Animation(animationSet.ElementAt(rand.Next(0, animationSet.Count))));
            entity.Components.Add(new Render());
            entity.Components.Add(new State());

            var path = PathData.GetRandomPath();

            entity.Components.Add(new EnemyAi(path, level));

            var speed = 5 + (level * 2);
            entity.Components.Add(new Position { X = (int)path.FirstOrDefault().X, Y = (int)path.FirstOrDefault().Y, Speed = speed, Width = 256, Height = 256 });
            Console.WriteLine($"Spawned Enemy Level: {level}");
            return entity;
        }

        #region Food/Water

        public static Entity CreateWaterDish()
        {
            var entity = new Entity();
            var rand = new Random();

            entity.Components.Add(new Render("src/Assets/scene/bowl_water_full.png"));
            entity.Components.Add(new Position { X = 2850, Y = 3400, Speed = 5, Width = 256, Height = 256 });

            return entity;
        }

        // Food and water:
        // 2850, 3300
        // 3050, 3300

        public static Entity CreateFoodDish()
        {
            var entity = new Entity();
            var rand = new Random();

            entity.Components.Add(new Render("src/Assets/scene/bowl_food_full.png"));
            entity.Components.Add(new Position { X = 3050, Y = 3400, Speed = 5, Width = 256, Height = 256 });

            return entity;
        }
        #endregion

        #region Fences
        public static Entity CreateWestFence()
        {
            var entity = new Entity();
            var rand = new Random();

            entity.Components.Add(new Position { X = 982, Y = 1152, Speed = 0, Width = 3112, Height = 2116 });
            entity.Components.Add(new Collision() { CollisionState = CollisionStates.Static });

            return entity;

            // West wall
            // 690, 480     770, 480
            // 690, 4096    770, 4096
        }
        public static Entity CreateNorthFence()
        {
            var entity = new Entity();
            // North wall
            // 690, 476    4094, 476
            // 690, 558    4097, 558

            return entity;
        }

        #endregion
        public static Entity CreateHouse()
        {
            var entity = new Entity();
            return entity;
            /// House:
            /// 982, 1152     4094, 1150
            /// 980, 3268     4093, 3267
        }

    }
}
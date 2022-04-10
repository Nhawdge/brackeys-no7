using System.Collections.Generic;
using JustWind.Components;
using Raylib_CsLo;

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
            entity.Components.Add(new Collision<CircleBoundType>()
            {
                CollisionState = CollisionStates.Dynamic,
                BoundType = new CircleBoundType()
                {
                    Radius = 55
                }
            });

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

            entity.Components.Add(new Position { X = 690, Y = 480, Speed = 0, Width = 80, Height = 3616 });
            entity.Components.Add(new Collision<RectangleBoundType>()
            {
                CollisionState = CollisionStates.Static,
                BoundType = new RectangleBoundType() { Rectangle = new Rectangle(690, 480, 80, 3616) }
            });

            return entity;

            // West wall
            // 690, 480     770, 480
            // 690, 4096    770, 4096
            // width = 80
            // height = 3616
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
            var rand = new Random();

            entity.Components.Add(new Position { X = 2538, Y = 2210, Speed = 0, Width = 2116, Height = 3112 });
            //entity.Components.Add(new Render("src/Assets/scene/roof.png"));
            entity.Components.Add(new Collision<RectangleBoundType>()
            {
                CollisionState = CollisionStates.Static,
                BoundType = new RectangleBoundType() { Rectangle = new Rectangle(982, 1152, 3112, 2115) }
            });

            return entity;

            /// House:
            /// 982, 1152     4094, 1150
            /// 980, 3268     4093, 3267

            // width = 3112
            // height = 2115;


        }

    }
}
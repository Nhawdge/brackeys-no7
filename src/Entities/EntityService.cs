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

            return entity;
        }

        public static Entity CreateRandomEnemy()
        {
            var entity = new Entity();
            var rand = new Random();

            //entity.Components.Add(new Position { X = Raylib_CsLo.Raylib.GetScreenWidth() / 2, Y = Raylib_CsLo.Raylib.GetScreenHeight() / 2, Speed = 5, Width = 256, Height = 256 });
            entity.Components.Add(new Animation(AnimationData.ScaryOptions.ElementAt(rand.Next(0, AnimationData.ScaryOptions.Count))));
            entity.Components.Add(new Render());
            entity.Components.Add(new State());

            var path = PathData.SideWalk;

            entity.Components.Add(new EnemyAi(path));
            entity.Components.Add(new Position { X = (int)path.FirstOrDefault().X, Y = (int)path.FirstOrDefault().Y, Speed = 5, Width = 256, Height = 256 });

            return entity;
        }
    }
}
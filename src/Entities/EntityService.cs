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

            entity.Components.Add(new Position { X = Raylib_CsLo.Raylib.GetScreenWidth() / 2, Y = Raylib_CsLo.Raylib.GetScreenHeight() / 2, Speed = 5, Width = 256, Height = 256 });
            entity.Components.Add(new Render("src/Assets/dog/dog_wag1.png"));
            entity.Components.Add(new Animation());
            entity.Components.Add(new Controllable());
            entity.Components.Add(new State());

            return entity;
        }

        public static Entity CreateEnemy()
        {
            var entity = new Entity();
            var rand = new Random();

            entity.Components.Add(new Position { X = Raylib_CsLo.Raylib.GetScreenWidth() / 2, Y = Raylib_CsLo.Raylib.GetScreenHeight() / 2, Speed = 5, Width = 256, Height = 256 });
            entity.Components.Add(new Render("src/Assets/dog/dog_wag1.png"));
            entity.Components.Add(new Animation());
            
            entity.Components.Add(new State());
            entity.Components.Add(new EnemyAi());

            return entity;
        }
    }
}
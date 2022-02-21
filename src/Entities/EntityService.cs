using JustWind.Components;

namespace JustWind.Entities
{
    public static class EntityService
    {

        public static Entity CreatePlayer()
        {
            var entity = new Entity();
            var rand = new Random();

            entity.Components.Add(new Position { X = 50, Y = 400, Speed = 5, Width = 256, Height = 256 });
            entity.Components.Add(new Render("src/Assets/dog.png"));
            entity.Components.Add(new Controllable());

            return entity;
        }

    }
}
using ZombieEscape.Components;

namespace ZombieEscape.Entities
{
    public static class EntityService
    {
       
        public static Entity CreatePlayer()
        {
            var entity = new Entity();
            var rand = new Random();

            entity.Components.Add(new Position { X = rand.Next(100, 300), Y = rand.Next(100, 300), Speed = 5, Width = 64, Height = 64 });
            entity.Components.Add(new Render("src/Assets/rancher.png"));
            entity.Components.Add(new Controllable());

            return entity;
        }
 
    }
}
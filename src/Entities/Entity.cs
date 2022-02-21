using System;
using System.Collections.Generic;
using System.Linq;
using ZombieEscape.Components;

namespace ZombieEscape.Entities
{
    public class Entity
    {
        public List<Component> Components = new();
        public Entity()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; init; }

        public T GetComponent<T>() where T : Component
        {
            return Components.OfType<T>().FirstOrDefault();

        }
        public IEnumerable<T> GetComponents<T>()
        {
            var components = Components.Where(x => x.GetType() == typeof(T)).Select(x => (T)Convert.ChangeType(x, typeof(T)));
            return components;
        }
    }
}
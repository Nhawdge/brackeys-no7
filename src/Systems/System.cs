using System.Collections.Generic;
using JustWind.Entities;

namespace JustWind.Systems
{
    public abstract class System
    {
        protected Engine Engine { get; set; }
        public System(Engine engine)
        {
            Engine = engine;
        }
        public abstract void Load();
        public abstract void Update(List<Entity> allEntities);
    }
}


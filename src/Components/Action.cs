using System;
using System.Numerics;
namespace ZombieEscape.Components
{
    public class Action : Component
    {
        public Actions ActiveAction { get; set; }
        public Render Render { get; set; }
        public int Duration { get; set; }
        public int CountDirection { get; set; } = 3;
        public Vector2 Target { get; set; }
        public Vector2 DrawPoint { get; set; }
        public Guid AttachedEntity { get; set; }
 
    }

    public enum Actions
    {
        Yell,
        Lasso
    }
}
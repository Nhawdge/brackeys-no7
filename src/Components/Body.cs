namespace ZombieEscape.Components
{
    public class Body : Component
    {
        public BodyType Type { get; set; }
    }

    public enum BodyType
    {
        Static,
        Dynamic,
    }
}
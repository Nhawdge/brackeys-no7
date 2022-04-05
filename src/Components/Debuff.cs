namespace JustWind.Components
{
    public class Debuff : Component
    {
        public Debuffs Type;
        public float Amount = 1;
    }

    public enum Debuffs
    {
        AmplifyDamage
    }
}
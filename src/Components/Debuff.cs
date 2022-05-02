namespace JustWind.Components
{
    public class Debuff<T> : Component where T : AbstractDebuffType
    {
        public float Value = 1;
        public T DebuffType;
    }

    public abstract class AbstractDebuffType
    {
    }

    public class DamageAmplify : AbstractDebuffType
    {
        public Actions ActionToAmplify;
    }
}
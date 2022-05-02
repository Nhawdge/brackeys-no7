namespace JustWind.Components
{
    public class Buff<T> : Component where T : AbstractBuffType
    {
        public T BuffType;
        public float Value = 1;

    }

    public abstract class AbstractBuffType
    {
    }

    public class SpeedBuff : AbstractBuffType
    {
    }
}
namespace JustWind.Components
{
    public class Act : Component
    {
        public Act(Actions action)
        {
            switch (action)
            {
                case Actions.Growl:
                    Action = Actions.Growl;
                    DamagePerTick = 5;
                    DurationInMs = 2f;
                    TotalDamageTicks = 4;
                    CooldownInSeconds = 2f;
                    break;
                case Actions.Bark:
                    Action = Actions.Bark;
                    DamagePerTick = 75;
                    DurationInMs = 1f;
                    TotalDamageTicks = 1;
                    CooldownInSeconds = 0.5f;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public Actions Action;
        public float ActionTimer = 0;
        public float DurationInMs;
        public int DamagePerTick;
        public float TotalDamageTicks;
        public float CooldownInSeconds;
    }

    public enum Actions
    {
        Bark,
        Growl
    }

}
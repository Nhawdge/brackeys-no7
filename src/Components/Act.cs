using System;
using System.Numerics;

namespace JustWind.Components
{
    public class Act : Component
    {
        public Act(Actions action)
        {
            LastActionTime = 0;
            switch (action)
            {
                case Actions.Growl:
                    Action = Actions.Growl;
                    DamagePerTick = 5;
                    DurationInSeconds = 2;
                    TotalDamageTicks = 4;
                    break;
                case Actions.Bark:
                    Action = Actions.Bark;
                    DamagePerTick = 75;
                    DurationInSeconds = 1;
                    TotalDamageTicks = 1;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public Actions Action;
        public double LastActionTime;
        public int DurationInSeconds;
        public int DamagePerTick;
        public int TotalDamageTicks;
    }

    public enum Actions
    {
        Bark,
        Growl
    }

}
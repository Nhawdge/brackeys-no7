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
                    DurationInMs = 2f;
                    TotalDamageTicks = 4;
                    break;
                case Actions.Bark:
                    Action = Actions.Bark;
                    DamagePerTick = 75;
                    DurationInMs = 1f;
                    TotalDamageTicks = 1;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public Actions Action;
        public double LastActionTime;
        public float DurationInMs;
        public int DamagePerTick;
        public float TotalDamageTicks;
    }

    public enum Actions
    {
        Bark,
        Growl
    }

}
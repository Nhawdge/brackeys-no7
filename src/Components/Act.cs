using System;
using System.Numerics;

namespace JustWind.Components
{
    public class Act : Component
    {
        public Actions Action { get; set; }
        public int Duration { get; set; }
    }

    public enum Actions
    {
        Bark,
        Growl
    }
}
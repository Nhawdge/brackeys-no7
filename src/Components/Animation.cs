using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Components
{
    public class Animation : Component
    {
        public List<Animation> Animations { get; set; } = new();
        public string Name { get; set; }
        public int Order { get; set; }
        public Texture Texture { get; set; }

    }
}
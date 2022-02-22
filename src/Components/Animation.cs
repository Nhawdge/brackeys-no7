using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Components
{
    public class Animation : Component
    {
        public Dictionary<int, Frame> Animations { get; set; } = new();
        public int Counter { get; set; }
        public int CurrentIndex { get; set; }
        public Animation()
        {
            var animations = new List<string>() {
                "src/Assets/dog/dog_wag1.png",
                "src/Assets/dog/dog_wag2.png",
                "src/Assets/dog/dog_wag3.png",
                "src/Assets/dog/dog_wag4.png",
            };

            var index = 0;
            foreach (var animation in animations)
            {
                var texture = LoadTexture(animation);
                var frame = new Frame(texture, 3);
                Animations.Add(index, frame);
                index++;
            }
        }
    }

    public record Frame(Texture Texture, int Delay);
}
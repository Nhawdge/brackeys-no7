using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Components
{
    public class Animation : Component
    {
        public Dictionary<int, Frame> Animations { get; set; } = new();
        public int Counter { get; set; }
        public int CurrentIndex { get; set; }
    }

    public record Frame(Texture Texture, int Delay);

    public static class AnimationData
    {
        public static Dictionary<int, Frame> DogWag = new Dictionary<int, Frame>() {
            { 0, new Frame(LoadTexture("src/Assets/dog/dog_wag1.png"), 3) },
            { 1, new Frame(LoadTexture("src/Assets/dog/dog_wag2.png"), 3) },
            { 2, new Frame(LoadTexture("src/Assets/dog/dog_wag3.png"), 3) },
            { 3, new Frame(LoadTexture("src/Assets/dog/dog_wag4.png"), 3) },
        };
        public static Dictionary<int, Frame> DogGrowl = new Dictionary<int, Frame>() {
            { 0, new Frame(LoadTexture("src/Assets/dog/dog_growl1.png"), 3) },
            { 1, new Frame(LoadTexture("src/Assets/dog/dog_growl2.png"), 3) },
        };
        public static Dictionary<int, Frame> DogSleep = new Dictionary<int, Frame>() {
            { 0, new Frame(LoadTexture("src/Assets/dog/dog_sleep1.png"), 3) },
            { 1, new Frame(LoadTexture("src/Assets/dog/dog_sleep2.png"), 3) },
            { 2, new Frame(LoadTexture("src/Assets/dog/dog_sleep3.png"), 3) },
        };
    }



}
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Components
{
    public class Animation : Component
    {
        public Dictionary<int, Frame> Animations = new();
        public int Counter;
        public int CurrentIndex;
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

        public static Dictionary<int, Frame> DogBark = new Dictionary<int, Frame>() {
            { 0, new Frame(LoadTexture("src/Assets/dog/dog_bark.png"), 3) },
        };

        public static Dictionary<int, Frame> AlienEye = new()
        {
            { 0, new Frame(LoadTexture("src/Assets/enemy/alien_eye1.png"), 3) },
            { 1, new Frame(LoadTexture("src/Assets/enemy/alien_eye2.png"), 3) },
            { 2, new Frame(LoadTexture("src/Assets/enemy/alien_eye3.png"), 3) },
            { 3, new Frame(LoadTexture("src/Assets/enemy/alien_eye4.png"), 3) },
        };

        public static Dictionary<int, Frame> Bat = new()
        {
            { 0, new Frame(LoadTexture("src/Assets/enemy/bat1.png"), 3) },
            { 1, new Frame(LoadTexture("src/Assets/enemy/bat2.png"), 3) },
        };

        public static Dictionary<int, Frame> Demon = new()
        {
            { 0, new Frame(LoadTexture("src/Assets/enemy/demon1.png"), 3) },
            { 1, new Frame(LoadTexture("src/Assets/enemy/demon2.png"), 3) },
        };

        public static Dictionary<int, Frame> ScooterKid = new()
        {
            { 0, new Frame(LoadTexture("src/Assets/enemy/scooter_kid1.png"), 3) },
            { 1, new Frame(LoadTexture("src/Assets/enemy/scooter_kid2.png"), 3) },
            { 2, new Frame(LoadTexture("src/Assets/enemy/scooter_kid3.png"), 3) },
            { 3, new Frame(LoadTexture("src/Assets/enemy/scooter_kid4.png"), 3) },
        };

        public static Dictionary<int, Frame> PersonA = new()
        {
            { 0, new Frame(LoadTexture("src/Assets/enemy/persona_walk1.png"), 3) },
            { 1, new Frame(LoadTexture("src/Assets/enemy/persona_walk2.png"), 3) },
            { 2, new Frame(LoadTexture("src/Assets/enemy/persona_walk3.png"), 3) },
            { 3, new Frame(LoadTexture("src/Assets/enemy/persona_walk4.png"), 3) },
        };
        public static Dictionary<int, Frame> PersonB = new()
        {
            { 0, new Frame(LoadTexture("src/Assets/enemy/personb_walk1.png"), 3) },
            { 1, new Frame(LoadTexture("src/Assets/enemy/personb_walk2.png"), 3) },
            { 2, new Frame(LoadTexture("src/Assets/enemy/personb_walk3.png"), 3) },
            { 3, new Frame(LoadTexture("src/Assets/enemy/personb_walk4.png"), 3) },
        };
    }
}
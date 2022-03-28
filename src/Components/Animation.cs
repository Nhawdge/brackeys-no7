using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Components
{
    public class Animation : Component
    {
        public Dictionary<int, Frame> Animations = new();
        public int Counter;
        public int CurrentIndex;

        public Animation(Dictionary<int, Frame> animations)
        {
            this.Animations = animations;
        }
    }

    public class Frame
    {
        private Texture _texture;
        public Texture Texture
        {
            get
            {

                return _texture;
            }
        }
        public string TexturePath;
        public int Delay;
        public Frame(string TexturePath, int Delay)
        {
            _texture = LoadTexture(TexturePath);
            this.Delay = Delay;
        }
    };

    public static class AnimationData
    {
        static AnimationData()
        {
            AlienEye = new()
            {
                { 0, new Frame("src/Assets/enemies/Alieneye1.png", 3) },
                { 1, new Frame("src/Assets/enemies/Alieneye2.png", 3) },
                { 2, new Frame("src/Assets/enemies/Alieneye3.png", 3) },
                { 3, new Frame("src/Assets/enemies/Alieneye4.png", 3) },
            };

            Bat = new()
            {
                { 0, new Frame("src/Assets/enemies/bat1.png", 3) },
                { 1, new Frame("src/Assets/enemies/bat2.png", 3) },
            };

            Demon = new()
            {
                { 0, new Frame("src/Assets/enemies/demon1.png", 3) },
                { 1, new Frame("src/Assets/enemies/demon2.png", 3) },
            };

            ScooterKid = new()
            {
                { 0, new Frame("src/Assets/enemies/scooter_kid1.png", 3) },
                { 1, new Frame("src/Assets/enemies/scooter_kid2.png", 3) },
                { 2, new Frame("src/Assets/enemies/scooter_kid3.png", 3) },
                { 3, new Frame("src/Assets/enemies/scooter_kid4.png", 3) },
            };

            PersonA = new()
            {
                { 0, new Frame("src/Assets/enemies/persona_walk1.png", 3) },
                { 1, new Frame("src/Assets/enemies/persona_walk2.png", 3) },
                { 2, new Frame("src/Assets/enemies/persona_walk3.png", 3) },
                { 3, new Frame("src/Assets/enemies/persona_walk4.png", 3) },
            };
            PersonB = new()
            {
                { 0, new Frame("src/Assets/enemies/personb_walk1.png", 3) },
                { 1, new Frame("src/Assets/enemies/personb_walk2.png", 3) },
                { 2, new Frame("src/Assets/enemies/personb_walk3.png", 3) },
                { 3, new Frame("src/Assets/enemies/personb_walk4.png", 3) },
            };

            ScaryOptions = new() { AnimationData.AlienEye, AnimationData.Bat, AnimationData.Demon };
            EnemyOptions = new() { AnimationData.PersonA, AnimationData.PersonB, AnimationData.ScooterKid };
        }
        public static List<Dictionary<int, Frame>> ScaryOptions;
        public static List<Dictionary<int, Frame>> EnemyOptions;
        public static readonly Dictionary<int, Frame> DogWag = new Dictionary<int, Frame>() {
            { 0, new Frame("src/Assets/dog/dog_wag1.png", 3) },
            { 1, new Frame("src/Assets/dog/dog_wag2.png", 3) },
            { 2, new Frame("src/Assets/dog/dog_wag3.png", 3) },
            { 3, new Frame("src/Assets/dog/dog_wag4.png", 3) },
        };

        public static readonly Dictionary<int, Frame> DogGrowl = new Dictionary<int, Frame>() {
            { 0, new Frame("src/Assets/dog/dog_growl1.png", 3) },
            { 1, new Frame("src/Assets/dog/dog_growl2.png", 3) },
        };

        public static readonly Dictionary<int, Frame> DogSleep = new Dictionary<int, Frame>() {
            { 0, new Frame("src/Assets/dog/dog_sleep1.png", 3) },
            { 1, new Frame("src/Assets/dog/dog_sleep2.png", 3) },
            { 2, new Frame("src/Assets/dog/dog_sleep3.png", 3) },
        };

        public static readonly Dictionary<int, Frame> DogBark = new Dictionary<int, Frame>() {
            { 0, new Frame("src/Assets/dog/dog_bark.png", 3) },
        };

        public static readonly Dictionary<int, Frame> AlienEye;

        public static readonly Dictionary<int, Frame> Bat;
        public static readonly Dictionary<int, Frame> Demon;
        public static readonly Dictionary<int, Frame> ScooterKid;

        public static readonly Dictionary<int, Frame> PersonA;
        public static readonly Dictionary<int, Frame> PersonB;
    }
}
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace JustWind.Components
{
    public class Animation : Component
    {
        public Dictionary<int, Frame> Animations = new();
        public float Counter;
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
        public float Delay;
        public Frame(string TexturePath, float Delay = 0.1f)
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
                { 0, new Frame("src/Assets/enemies/Alieneye1.png") },
                { 1, new Frame("src/Assets/enemies/Alieneye2.png") },
                { 2, new Frame("src/Assets/enemies/Alieneye3.png") },
                { 3, new Frame("src/Assets/enemies/Alieneye4.png") },
            };

            Bat = new()
            {
                { 0, new Frame("src/Assets/enemies/bat1.png") },
                { 1, new Frame("src/Assets/enemies/bat2.png") },
            };

            Demon = new()
            {
                { 0, new Frame("src/Assets/enemies/demon1.png") },
                { 1, new Frame("src/Assets/enemies/demon2.png") },
            };

            ScooterKid = new()
            {
                { 0, new Frame("src/Assets/enemies/scooter_kid1.png") },
                { 1, new Frame("src/Assets/enemies/scooter_kid2.png") },
                { 2, new Frame("src/Assets/enemies/scooter_kid3.png") },
                { 3, new Frame("src/Assets/enemies/scooter_kid4.png") },
            };

            PersonA = new()
            {
                { 0, new Frame("src/Assets/enemies/persona_walk1.png") },
                { 1, new Frame("src/Assets/enemies/persona_walk2.png") },
                { 2, new Frame("src/Assets/enemies/persona_walk3.png") },
                { 3, new Frame("src/Assets/enemies/persona_walk4.png") },
            };

            PersonB = new()
            {
                { 0, new Frame("src/Assets/enemies/personb_walk1.png") },
                { 1, new Frame("src/Assets/enemies/personb_walk2.png") },
                { 2, new Frame("src/Assets/enemies/personb_walk3.png") },
                { 3, new Frame("src/Assets/enemies/personb_walk4.png") },
            };

            GreenGuy = new()
            {
                { 0, new Frame("src/Assets/enemies/AlienGreen1.png") },
                { 1, new Frame("src/Assets/enemies/AlienGreen2.png") },
                { 2, new Frame("src/Assets/enemies/AlienGreen3.png") },
                { 3, new Frame("src/Assets/enemies/AlienGreen4.png") },
            };

            DinoRed = new()
            {
                { 0, new Frame("src/Assets/enemies/trexgreen1.png") },
                { 1, new Frame("src/Assets/enemies/trexgreen2.png") },
                { 2, new Frame("src/Assets/enemies/trexgreen3.png") },
                { 3, new Frame("src/Assets/enemies/trexgreen4.png") },
            };

            DinoGreen = new(){
                { 0, new Frame("src/Assets/enemies/trex1.png") },
                { 1, new Frame("src/Assets/enemies/trex2.png") },
                { 2, new Frame("src/Assets/enemies/trex3.png") },
                { 3, new Frame("src/Assets/enemies/trex4.png") },
            };

            Transition = new() {
                { 0, new Frame("src/Assets/Enemies/transition1.png" )},
                { 1, new Frame("src/Assets/Enemies/transition2.png" )},
            };

            ScaryOptionsL1 = new() { AnimationData.Demon, AnimationData.Bat };
            ScaryOptionsL2 = new() { AnimationData.AlienEye, AnimationData.GreenGuy };
            ScaryOptionsL3 = new() { AnimationData.DinoRed, AnimationData.DinoGreen };


            //ScaryOptions = new() { AnimationData.AlienEye, AnimationData.Bat, AnimationData.Demon };
            EnemyOptions = new() { AnimationData.PersonA, AnimationData.PersonB, AnimationData.ScooterKid };
        }
        public static List<Dictionary<int, Frame>> ScaryOptionsL1;
        public static List<Dictionary<int, Frame>> ScaryOptionsL2;
        public static List<Dictionary<int, Frame>> ScaryOptionsL3;
        public static List<Dictionary<int, Frame>> EnemyOptions;
        public static readonly Dictionary<int, Frame> DogWag = new Dictionary<int, Frame>() {
            { 0, new Frame("src/Assets/dog/dog_wag1.png") },
            { 1, new Frame("src/Assets/dog/dog_wag2.png") },
            { 2, new Frame("src/Assets/dog/dog_wag3.png") },
            { 3, new Frame("src/Assets/dog/dog_wag4.png") },
        };

        public static readonly Dictionary<int, Frame> DogGrowl = new Dictionary<int, Frame>() {
            { 0, new Frame("src/Assets/dog/dog_growl1.png") },
            { 1, new Frame("src/Assets/dog/dog_growl2.png") },
        };

        public static readonly Dictionary<int, Frame> DogSleep = new Dictionary<int, Frame>() {
            { 0, new Frame("src/Assets/dog/dog_sleep1.png") },
            { 1, new Frame("src/Assets/dog/dog_sleep2.png") },
            { 2, new Frame("src/Assets/dog/dog_sleep3.png") },
        };

        public static readonly Dictionary<int, Frame> DogBark = new Dictionary<int, Frame>() {
            { 0, new Frame("src/Assets/dog/dog_bark.png") },
        };

        public static readonly Dictionary<int, Frame> SunTimer = new Dictionary<int, Frame>() {
            { 0, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0000.png", 15) },
            { 1, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0001.png", 15) },
            { 2, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0002.png", 15) },
            { 3, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0003.png", 15) },
            { 4, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0004.png", 15) },
            { 5, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0005.png", 15) },
            { 6, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0006.png", 15) },
            { 7, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0007.png", 15) },
            { 8, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0008.png", 15) },
            { 9, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0009.png", 15) },
            { 10, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0010.png", 15) },
            { 11, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0011.png", 15) },
            { 12, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0012.png", 15) },
            { 13, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0013.png", 15) },
            { 14, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0014.png", 15) },
            { 15, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0015.png", 15) },
            { 16, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0016.png", 15) },
            { 17, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0017.png", 15) },
            { 18, new Frame("src/Assets/scene/Sun_Timer/Sun_Timer0018.png", 15) },
         };

        #region Demons (L1)
        public static readonly Dictionary<int, Frame> Bat;
        public static readonly Dictionary<int, Frame> Demon;

        #endregion

        #region Aliens (L2)

        public static readonly Dictionary<int, Frame> AlienEye;
        public static readonly Dictionary<int, Frame> GreenGuy;

        #endregion

        #region  Dinosaurs (L3)

        public static readonly Dictionary<int, Frame> DinoGreen;
        public static readonly Dictionary<int, Frame> DinoRed;

        #endregion

        #region  Safe

        public static readonly Dictionary<int, Frame> ScooterKid;
        public static readonly Dictionary<int, Frame> PersonA;
        public static readonly Dictionary<int, Frame> PersonB;
        public static readonly Dictionary<int, Frame> Transition;

        #endregion

    }
}
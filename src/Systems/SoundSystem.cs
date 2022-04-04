using System.ComponentModel.Design;
using System.Collections;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.Generic;
using JustWind.Entities;
using Raylib_CsLo;
using JustWind.Components;

namespace JustWind.Systems
{
    public class SoundSystem : System
    {
        private Music CurrentMusic;
        public SoundSystem(Engine engine) : base(engine)
        {
            CurrentMusic = SoundData.TitleMusic;
        }

        public override void Load()
        {
            CurrentMusic = SoundData.TitleMusic;
            Raylib.SetMusicVolume(CurrentMusic, 0.5f);
            Raylib.PlayMusicStream(CurrentMusic);
        }

        public override void Update(List<Entity> allEntities)
        {
            Raylib.UpdateMusicStream(CurrentMusic);
            var singleton = Engine.Singleton.GetComponent<Singleton>();

            if (singleton.State == GameState.Menu)
            {
                if (!CurrentMusic.Equals(SoundData.TitleMusic))
                {
                    Raylib.StopMusicStream(CurrentMusic);
                    CurrentMusic = SoundData.TitleMusic;
                    Raylib.PlayMusicStream(CurrentMusic);
                }
            }
            else if (!CurrentMusic.Equals(SoundData.GameMusic))
            {
                Raylib.StopMusicStream(CurrentMusic);
                CurrentMusic = SoundData.GameMusic;
                Raylib.PlayMusicStream(CurrentMusic);
            }


            var soundedEntities = allEntities.Where(x => x.HasTypes(typeof(ActiveSound)));
            foreach (var entity in soundedEntities)
            {
                var sound = entity.GetComponent<ActiveSound>();

                Raylib.PlaySound(sound.SoundToPlay);
                entity.Components.Remove(sound);
            }
        }
    }
    public static class SoundData
    {
        public static Music TitleMusic;
        public static Music GameMusic;
        public static Sound Bark;
        public static Sound Growl;


        static SoundData()
        {
            Raylib.InitAudioDevice();

            TitleMusic = Raylib.LoadMusicStream("src/Assets/audio/title.wav");
            GameMusic = Raylib.LoadMusicStream("src/Assets/audio/muzac.wav");
            Bark = Raylib.LoadSound("src/Assets/audio/bark.wav");
            Growl = Raylib.LoadSound("src/Assets/audio/growl.wav");
        }
    }
}
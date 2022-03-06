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
        Music BackgroundMusic { get; set; }
        public SoundSystem(Engine engine) : base(engine)
        {
        }
        public override void Load()
        {
            // Raylib.InitAudioDevice();
            // BackgroundMusic = Raylib.LoadMusicStream("src/assets/background.wav");
            // Raylib.SetMusicVolume(BackgroundMusic, 0.1f);
            // Raylib.PlayMusicStream(BackgroundMusic);
        }

        public override void Update(List<Entity> allEntities)
        {
            var singleton = Engine.Singleton.GetComponent<Singleton>();

            Raylib.UpdateMusicStream(BackgroundMusic);
        }
    }
}
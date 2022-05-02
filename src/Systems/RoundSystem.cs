using JustWind.Components;
using JustWind.Entities;
using Raylib_CsLo;

namespace JustWind.Systems
{
    public class RoundSystem : JustWind.Systems.System
    {
        public RoundSystem(Engine engine) : base(engine)
        {

        }

        public override void Load()
        {

        }

        public override void Update(List<Entity> allEntities)
        {
            var singleton = Engine.Singleton.GetComponent<Singleton>();
            if (singleton.State == GameState.Game)
            {
                singleton.Stats.RoundTimer += Raylib.GetFrameTime();

                if (singleton.Stats.RoundTimer > singleton.Stats.RoundDuration)
                {
                    singleton.State = GameState.GameWin;
                    singleton.Stats.RoundTimer = 0;
                    singleton.Stats.Round++;
                }

            }
        }
    }
}
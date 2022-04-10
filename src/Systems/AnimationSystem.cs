using JustWind.Components;
using JustWind.Entities;

namespace JustWind.Systems
{
    public class AnimationSystem : JustWind.Systems.System
    {
        public AnimationSystem(Engine engine) : base(engine)
        {
        }

        public override void Load()
        {
        }

        public override void Update(List<Entity> allEntities)
        {
            var singleton = Engine.Singleton.GetComponent<Singleton>();
            if (singleton.State != GameState.Game)
            {
                return;
            }

            foreach (var entity in allEntities.Where(x => x.HasTypes(typeof(Animation), typeof(Render))))
            {
                var myAnimation = entity.GetComponent<Animation>();
                var myRender = entity.GetComponent<Render>();
                myAnimation.Counter += Raylib_CsLo.Raylib.GetFrameTime();
                
                if (myAnimation.Animations.Count() > 0)
                {
                    var currentFrame = myAnimation.Animations.ElementAtOrDefault(myAnimation.CurrentIndex).Value;

                    if (currentFrame == null)
                    {
                        currentFrame = myAnimation.Animations.First().Value;
                    }

                    if (myAnimation.Counter > currentFrame.Delay)
                    {
                        myAnimation.Counter = 0;
                        myAnimation.CurrentIndex++;
                        var totalFrames = myAnimation.Animations.Count();

                        if (myAnimation.CurrentIndex >= totalFrames)
                        {
                            myAnimation.CurrentIndex = 0;
                        }
                        myRender.Texture = myAnimation.Animations.ElementAt(myAnimation.CurrentIndex).Value.Texture;
                        myRender.UpdateRect();
                    }
                }
            }
        }
    }
}
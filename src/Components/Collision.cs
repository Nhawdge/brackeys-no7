using Raylib_CsLo;

namespace JustWind.Components
{
    public class Collision<T> : Component where T : AbstractBoundingType
    {
        public CollisionStates CollisionState;
        public T BoundType;
    }

    public abstract class AbstractBoundingType
    {
    }

    public class CircleBoundType : AbstractBoundingType
    {
        public float Radius;
    }

    public class RectangleBoundType : AbstractBoundingType
    {
        public Rectangle Rectangle;
    }

    public enum CollisionStates
    {
        None,
        Static,
        Dynamic
    }
    /// House:
    /// 982, 1152     4094, 1150
    /// 980, 3268     4093, 3267

    // Food and water:
    // 2850, 3300
    // 3050, 3300

    // West wall
    // 690, 480     770, 480
    // 690, 4096    770, 4096

    // North wall
    // 690, 476    4094, 476
    // 690, 558    4097, 558

}
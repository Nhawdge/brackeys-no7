namespace JustWind.Components
{
    public class State : Component
    {
        public CharacterState CurrentState = CharacterState.Idle;
    }

    public enum CharacterState
    {
        Idle,
        Moving,
        Bark,
        Growl,
        Sleep,
    }
}
namespace JustWind.Components
{
    public class State : Component
    {
        public CharacterState CurrentState { get; set; } = CharacterState.Idle;
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
namespace JustWind.Components
{
    public class Singleton : Component
    {
        public GameState State = GameState.Menu;
        public int Score = 0;
    }

    public enum GameState
    {
        Menu,
        Game,
        MenuHowToPlay,
        MenuCredits,
        Exit
    }
}
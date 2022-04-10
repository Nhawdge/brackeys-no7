namespace JustWind.Components
{
    public class Singleton : Component
    {
        public GameState State = GameState.Menu;
        public int Score = 0;
        public float HouseSafety = 1000;
        public float MaxHouseSafety = 1000;
        public float LastSpawnTime = 0;
    }

    public enum GameState
    {
        Menu,
        Game,
        GameLoss,
        GameWin,
        MenuHowToPlay,
        MenuCredits,
        Paused,
        Exit
    }
}
namespace JustWind.Components
{
    public class Singleton : Component
    {
        public GameState State = GameState.Menu;
        public int Score = 0;
        public int HouseSafety = 1000;
        public int MaxHouseSafety = 1000;
        public int LastSpawnTime = 0;
    }

    public enum GameState
    {
        Menu,
        Game,
        MenuHowToPlay,
        MenuCredits,
        Paused,
        Exit
    }
}
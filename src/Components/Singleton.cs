namespace JustWind.Components
{
    public class Singleton : Component
    {
        public GameState State = GameState.Menu;
        public int Score = 0;
        public float HouseSafety = 1000;
        public float MaxHouseSafety = 1000;
        public float LastSpawnTime = 0;

        public GameStats Stats = new GameStats();
        public UserOptions Options = new UserOptions();
    }

    public class GameStats
    {
        public int Round = 1;
        public float RoundTimer = 0;
        public float RoundDuration = 90;
    }

    public class UserOptions
    {
        public float MusicVolume { get; set; } = 1f;
        public float SoundVolume { get; set; } = 1f;
    }

    public enum GameState
    {
        Menu,
        Game,
        GameLoss,
        GameWin,
        NextRound,
        MenuHowToPlay,
        MenuCredits,
        Paused,
        Exit,
    }
}
namespace JustWind.Components
{
    public class Message : Component
    {
        public Message(string message)
        {
            Info = message;
        }

        public string Info { get; set; } = string.Empty;

    }
}
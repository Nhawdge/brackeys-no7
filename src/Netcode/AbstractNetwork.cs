
using JustWind.Components;

namespace JustWind.Netcode
{
    public abstract class AbstractNetwork
    {
        internal Engine engine;
        internal bool IsRunning = true;
        internal string Payload { get; set; } = string.Empty;
        internal bool ReadyToSend { get; set; } = false;

        public AbstractNetwork(Engine engine)
        {
            this.engine = engine;
        }

        public void SendString(string message)
        {
            this.Payload = message;
            this.ReadyToSend = true;
        }

        public void SaveMessage(string message)
        {
            engine.Singleton.Components.Add(new Message(message));
        }

        public void Stop()
        {
            this.IsRunning = false;
        }
        public abstract void Start();

    }
}
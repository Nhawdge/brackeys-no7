
namespace JustWind.Netcode
{
    public interface INetwork
    {
        void SendString(string message);
        public void Start();
        public void Stop();
    }
}
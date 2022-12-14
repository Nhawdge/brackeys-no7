using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JustWind.Netcode
{
    public class NetClient : AbstractNetwork
    {
        public NetClient(Engine engine) : base(engine)
        {
        }

        public override async void Start()
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new(ipAddress, 11_000);

            using Socket client = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            var isRunning = true;
            await client.ConnectAsync(ipEndPoint);
            while (isRunning)
            {
                var message = this.Payload;
                if (ReadyToSend)
                {
                    this.ReadyToSend = false;
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    await client.SendAsync(messageBytes, SocketFlags.None);
                    this.SaveMessage($"Servr: {message}");
                }

                var buffer = new byte[1_024];
                client.ReceiveAsync(buffer, SocketFlags.None).ContinueWith(x =>
                {
                    var response = Encoding.UTF8.GetString(buffer, 0, x.Result);
                    this.SaveMessage($"Client: {response}");
                });
            }
            client.Shutdown(SocketShutdown.Both);
        }
    }
}
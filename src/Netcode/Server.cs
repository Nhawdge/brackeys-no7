using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JustWind.Netcode
{
    public class NetServer : AbstractNetwork
    {
        public NetServer(Engine engine) : base(engine)
        {
        }

        public override async void Start()
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new(ipAddress, 11_000);

            using Socket listener = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            listener.Bind(ipEndPoint);
            listener.Listen(100);

            var handler = await listener.AcceptAsync();
            var listenTask = Task.Run(async () =>
            {
                var buffer = new byte[1_024];
                while (true)
                {
                    var received = await handler.ReceiveAsync(buffer, SocketFlags.None);

                    var response = Encoding.UTF8.GetString(buffer, 0, received);
                    this.SaveMessage($"Client: {response}");

                };
            });

            while (true)
            {
                var message = this.Payload;
                if (ReadyToSend)
                {
                    this.ReadyToSend = false;
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    await handler.SendAsync(messageBytes, SocketFlags.None);
                    this.SaveMessage($"Server: {message}");
                }
            }

        }
    }
}
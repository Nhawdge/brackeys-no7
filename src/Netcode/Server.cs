using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JustWind.Netcode
{
    public class NetServer : INetwork
    {
        private bool IsRunning = true;
        private Engine engine;

        public NetServer(Engine engine)
        {
            this.engine = engine;
        }

        public string Payload { get; set; }

        public void SendString(string message)
        {
            this.Payload = message;
            Console.WriteLine("Sending string from server");
        }

        public async void Start()
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            IPEndPoint ipEndPoint = new(ipAddress, 11_000);

            using Socket listener = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            Console.WriteLine("Server Started");

            listener.Bind(ipEndPoint);
            listener.Listen(100);
            Console.WriteLine("Binding port");

            var handler = await listener.AcceptAsync();
            while (IsRunning)
            {
                Console.WriteLine("Stuck 1");

                var message = "";
                if (!string.IsNullOrEmpty(Payload))
                {
                    message = Payload;
                }
                Console.WriteLine($"Sending: {message}");

                // Receive message.
                var buffer = new byte[1_024];
                Console.WriteLine("Stuck 2");
                var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                Console.WriteLine("Stuck 3");

                var response = Encoding.UTF8.GetString(buffer, 0, received);

                Console.WriteLine($"Received: {response}");
                var eom = "<|EOM|>";
                if (response.IndexOf(eom) > -1 /* is end of message */)
                {
                    Console.WriteLine(
                        $"Socket server received message: \"{response.Replace(eom, "")}\"");

                    var ackMessage = "<|ACK|>";
                    var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                    await handler.SendAsync(echoBytes, 0);
                    Console.WriteLine(
                        $"Socket server sent acknowledgment: \"{ackMessage}\"");
                    //break;
                }
                // Sample output:
                //    Socket server received message: "Hi friends 👋!"
                //    Socket server sent acknowledgment: "<|ACK|>"
            }

        }

        public void Stop()
        {
            this.IsRunning = false;
        }

    }
}
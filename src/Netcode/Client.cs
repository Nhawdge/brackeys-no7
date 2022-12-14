using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JustWind.Netcode
{
    public class NetClient : INetwork
    {
        private bool IsRunning = true;
        private Engine engine;

        private string Payload { get; set; } = string.Empty;
        public NetClient(Engine engine)
        {
            this.engine = engine;
        }

        public void SendString(string message)
        {
            this.Payload = message;
            Console.WriteLine("Sending string from client");
        }

        public async void Start()
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new(ipAddress, 11_000);

            using Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            await client.ConnectAsync(ipEndPoint);
            while (IsRunning)
            {
                var message = "";
                if (!string.IsNullOrEmpty(Payload))
                {
                    message = Payload;
                }
                Console.WriteLine($"Sending: {message}");
                // Send message.
                var messageBytes = Encoding.UTF8.GetBytes(message);
                _ = await client.SendAsync(messageBytes, SocketFlags.None);
                Console.WriteLine($"Socket client sent message: \"{message}\"");

                // Receive ack.
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                Console.WriteLine($"Received: {response}");
                if (response == "<|ACK|>")
                {
                    Console.WriteLine(
                        $"Socket client received acknowledgment: \"{response}\"");
                    //break;
                }
                // Sample output:
                //     Socket client sent message: "Hi friends 👋!<|EOM|>"
                //     Socket client received acknowledgment: "<|ACK|>"
            }
            client.Shutdown(SocketShutdown.Both);

        }

        public void Stop()
        {
            this.IsRunning = false;
        }

    }
}
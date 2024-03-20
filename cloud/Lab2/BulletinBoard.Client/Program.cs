using System.Net.WebSockets;
using System.Text;

var ws = new ClientWebSocket();
await ws.ConnectAsync(new Uri("ws://localhost:5175/api/web-socket"), CancellationToken.None);

var buffer = new byte[1024 * 4];
while (true)
{
    var message = Console.ReadLine();
    var bytes = Encoding.UTF8.GetBytes(message);
    await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    var response = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, response.Count));
}
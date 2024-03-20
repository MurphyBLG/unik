using BulletinBoard.Server.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace BulletinBoard.Server.Controllers;

[Route("api/web-socket")]
[ApiController]
public class WebSocketController(ILogger<WebSocketController> logger, IBus bus, IRequestClient<GetAllAdsContract> getAllAdsRequestClient, IRequestClient<CreateAdContract> createAdRequestClient) : ControllerBase
{
    public async Task<IActionResult> OpenWs(CancellationToken cancellationToken = default)
    {
        try
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
                throw new Exception("Not a ws request!");

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            var buffer = new byte[1024];
            while (true)
            {
                var message = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                if (message.MessageType != WebSocketMessageType.Text)
                {
                    await webSocket.SendAsync(GetWrongMessageTypeError(), WebSocketMessageType.Text, true, cancellationToken);
                    continue;
                }

                var command = Encoding.UTF8.GetString(buffer, 0, message.Count);
                switch (command)
                {
                    case "LIST":
                        var tmp = await getAllAdsRequestClient.GetResponse<GetAllAdsResponse>(new GetAllAdsContract());
                        await webSocket.SendAsync(GetMessage(tmp.Message.Message), WebSocketMessageType.Text, true, cancellationToken);
                        break;
                    case "":
                        return Ok();
                    default:
                        var tmp1 = await createAdRequestClient.GetResponse<CreateAdResponse>(new CreateAdContract { Advertisement = command });
                        await webSocket.SendAsync(GetMessage(tmp1.Message.Message), WebSocketMessageType.Text, true, cancellationToken);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return Problem();
        }
    }

    private static ArraySegment<byte> GetWrongMessageTypeError()
    {
        var error = "Wrong message type!";
        var bytes = Encoding.UTF8.GetBytes(error);
        var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
        return arraySegment;
    }
    private static ArraySegment<byte> GetMessage(string message)
    {
        var bytes = Encoding.UTF8.GetBytes(message);
        var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
        return arraySegment;
    }
}

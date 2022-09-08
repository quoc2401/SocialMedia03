using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

namespace SocialMedia03.WEB.Controllers.API
{
    [ApiController]
    public class NotificationCenter : ControllerBase
    {

        private static Dictionary<String, WebSocket> wsList { get; } = new Dictionary<string, WebSocket>();
        [HttpGet("/ws/{uuid}")]
        public async Task Get([FromRoute] string uuid)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                Debug.WriteLine(uuid + " client's WebSocket connection established");
                if (wsList.TryGetValue(uuid, out WebSocket ws))
                    wsList[uuid] = webSocket;
                else
                    wsList.Add(uuid, webSocket);
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        public static void SendMessage(string msg, string uuid)
        {
            if (wsList.TryGetValue(uuid, out WebSocket w)) 
            {
                var ws = wsList[uuid];
                if (ws != null)
                {
                    var serverMsg = Encoding.UTF8.GetBytes(msg);
                    ws.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            

        }

        private async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            wsList.Remove(wsList.Where(w => w.Value == webSocket).Select(w => w.Key).Single());
        }
    }
}

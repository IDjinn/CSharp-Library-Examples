
using System.Net.WebSockets;
using System.Text;

var websocket = new ClientWebSocket();

await websocket.ConnectAsync(new Uri("wss://localhost:5001/"), CancellationToken.None);
var buffer = new byte[1024];
while (websocket.State == WebSocketState.Open)
{
    var result = await websocket.ReceiveAsync(buffer, CancellationToken.None);
    if(result.MessageType == WebSocketMessageType.Close)
    {
        await websocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }
    else
    {
        Console.WriteLine($"Mensagem recebida do servidor: {Encoding.ASCII.GetString(buffer,0, result.Count)}");
        await websocket.SendAsync(Encoding.ASCII.GetBytes($"Hello World: {DateTime.Now.ToLongTimeString()}"), WebSocketMessageType.Text, true, CancellationToken.None);
    }
    Thread.Sleep(TimeSpan.FromSeconds(1));
}
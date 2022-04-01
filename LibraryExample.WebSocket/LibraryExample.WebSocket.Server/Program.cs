using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpLogging();
app.UseWebSockets();

var buffer = new byte[1024];
app.MapGet("/", async (context) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var data = Encoding.ASCII.GetBytes($"Hello World! {DateTime.Now.ToLongTimeString()}");
        Console.WriteLine($"Sending data: {data}");
        
        var stopwatch = Stopwatch.StartNew();
        await webSocket.SendAsync(
            data,
            WebSocketMessageType.Text,
            true,
            context.RequestAborted
        );
        stopwatch.Stop();
        Console.WriteLine($"Sent data in {stopwatch.Elapsed}");
        while (true)
        {
            var result = await webSocket.ReceiveAsync(buffer, context.RequestAborted);
            if (result.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.ASCII.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received data: {message}");
                
                stopwatch = Stopwatch.StartNew();
                var newData = Encoding.ASCII.GetBytes($"Hello World! {DateTime.Now.ToLongTimeString()}");
                Console.WriteLine($"Sending data: {newData}");
                await webSocket.SendAsync(
                    newData,
                    WebSocketMessageType.Text,
                    true,
                    context.RequestAborted
                );
                stopwatch.Stop();
                Console.WriteLine($"Sent data in {stopwatch.Elapsed}");
            }
        }
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
    }
});
app.Run();
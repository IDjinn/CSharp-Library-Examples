using System.Diagnostics;
using LibraryExample.Redis.Client.API;
using StackExchange.Redis;

namespace LibraryExample.Redis.Client.Clients;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class RedisClient : IRedisClient
{
    private readonly ConnectionMultiplexer _connection;
    
    public RedisClient(ConnectionMultiplexer connection)
    {
        _connection = connection;
    }

    public void Dispose()
    {
        _connection.Close();
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.CloseAsync();
    }
}
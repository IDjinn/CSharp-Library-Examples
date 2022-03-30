using LibraryExample.Redis.Client.API;
using StackExchange.Redis;

namespace LibraryExample.Redis.Client.Clients;

public partial class RedisClient : IRedisClient
{
    public IDatabase GetDatabase(int database = -1)
    {
        return _connection.GetDatabase(database);
    }
}
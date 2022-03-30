using System.Net;
using StackExchange.Redis;

namespace LibraryExample.Redis.Client.API;

/// <summary>
/// Redis client API
/// </summary>
public interface IRedisClient: IDisposable, IAsyncDisposable
{
    public IDatabase GetDatabase(int database = -1);
}
using LibraryExample.Redis.Client.API.Exceptions;

namespace LibraryExample.Redis.Client.API;

/// <summary>
/// Redis client manager API.
/// </summary>
/// <typeparam name="TRedisClient">Type of redis client instance</typeparam>
public interface IRedisClientManager<out TRedisClient> where TRedisClient : IRedisClient
{
    /// <summary>
    /// Create a new RedisClient instance
    /// </summary>
    /// <exception cref="FailedRedisConnectionException">Fail while creating new redis connection</exception>
    /// <returns>IRedisClient</returns>
    public TRedisClient GetClient();
}
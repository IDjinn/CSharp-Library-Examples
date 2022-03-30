using LibraryExample.Redis.Client.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;

namespace LibraryExample.Redis.Client;

/// <summary>
/// Redis client factory and manager
/// </summary>
/// <remarks>This manager is flagged as generic type match for be able to support any type of extension <see cref="IRedisClient"/>,
/// just need configure this properly at services provider.</remarks>
/// <typeparam name="TRedisClient">Type instance of client to connect with redis.</typeparam>
public class RedisClientManager<TRedisClient> : IRedisClientManager<TRedisClient> where TRedisClient : class, IRedisClient
{
    private readonly ILogger<RedisClientManager<TRedisClient>> _logger;
    private readonly IRedisClientFactory<TRedisClient> _redisClientFactory;
    private readonly IConfiguration _configuration;

    public RedisClientManager(
            ILogger<RedisClientManager<TRedisClient>> logger,
            IRedisClientFactory<TRedisClient> redisClientFactory, 
            IConfiguration configuration
        )
    {
        _logger = logger;
        _redisClientFactory = redisClientFactory;
        _configuration = configuration;
    }

    public TRedisClient GetClient()
    {
        try
        {
            return _redisClientFactory.Create();
        }
        catch (Exception exception)
        {
            _logger.LogError("Error creating redis client with message '{Message}'", exception.Message, exception);
            throw;
        }
    }
}
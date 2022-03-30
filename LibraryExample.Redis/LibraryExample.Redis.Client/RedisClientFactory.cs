using LibraryExample.Redis.Client.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LibraryExample.Redis.Client;

public class RedisClientFactory<TRedisClient> : IRedisClientFactory<TRedisClient> where TRedisClient : IRedisClient
{
    private IServiceProvider _serviceProvider { get; }
    private IConfiguration _configuration { get; }

    public RedisClientFactory(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    /// <summary>
    /// Create new Redis client
    /// </summary>
    /// <exception cref="Exception">Thrown when client cannot be created</exception>
    /// <returns><see cref="TRedisClient"/></returns>
    public TRedisClient Create()
    {
        // TODO: factory doesn't need know about configuration, it should be injected
        return ActivatorUtilities.CreateInstance<TRedisClient>(
            _serviceProvider,
            ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis")) 
        );
    }
}
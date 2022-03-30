
using Microsoft.Extensions.Configuration;

namespace LibraryExample.Redis.Client.API;

public interface IRedisClientFactory<out T> where T:IRedisClient
{
    public T Create();
}
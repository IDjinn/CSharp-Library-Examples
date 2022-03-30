using LibraryExample.Redis.Client.API;
using Microsoft.AspNetCore.Mvc;

namespace LibraryExample.Redis.Client.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRedisClientManager<IRedisClient> _redisClientManager;
    public UsersController(IRedisClientManager<IRedisClient> redisClientManager)
    {
        _redisClientManager = redisClientManager;
    }
    public record User(string Nickname);

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<User> GetAll()
    {
        using var redisClient = _redisClientManager.GetClient();
        var users = new User[]{new (redisClient.GetDatabase().StringGet("users").ToString())};
        return users;
    }
}
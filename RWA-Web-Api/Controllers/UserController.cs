using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;


namespace RWA_Web_Api.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(User))]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetUsers();

        return Ok(users);
    }

}
using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthRepository _authRepository;

    public AuthController(ILogger<AuthController> logger, IAuthRepository authRepository)
    {
        _logger = logger;
        _authRepository = authRepository;
    }

    [HttpPost("/user/register")]
    public IActionResult Register(UserRegisterDto registerDto)
    {
        var response = _authRepository.Register(
            new User()
            {
                username = registerDto.Username,
                first_name = registerDto.FirstName, 
                last_name = registerDto.LastName,
                role = "user"
            }, registerDto.Password);

        return !response.Success ? BadRequest(response) : Ok(response);
    }
    
    [HttpPost("/user/login")]
    public IActionResult Login(UserLoginDto registerDto)
    {
        var response = _authRepository.Login(registerDto.Username, registerDto.Password);

        return !response.Success ? BadRequest(response) : Ok(response);
    }
}
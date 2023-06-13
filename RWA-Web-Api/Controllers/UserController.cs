using AutoMapper;
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
    private readonly IMapper _mapper;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository, IMapper mapper)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    public IActionResult GetUsers()
    {
        var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

        return Ok(users);
    }

}
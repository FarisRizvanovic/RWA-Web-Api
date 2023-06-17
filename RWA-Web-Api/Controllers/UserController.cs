using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;


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

    [HttpGet("/users/{page}/{searchTerm?}")]
    [ProducesResponseType(200, Type = typeof(PaginationResult<UserDto>))]
    public IActionResult GetUsers(int page, string? searchTerm)
    {
        if (page <= 0)
        {
            return BadRequest("Invalid page number");
        }

        var paginationResult = _userRepository.GetUsers(page, searchTerm);

        if (page > paginationResult.TotalPages && paginationResult.TotalPages!=0)
        {
            return NotFound();
        }

        var users = _mapper.Map<List<UserDto>>(paginationResult.Items);

        var result = new PaginationResult<UserDto>()
        {
            Page = paginationResult.Page,
            PageSize = paginationResult.PageSize,
            TotalItems = paginationResult.TotalItems,
            TotalPages = paginationResult.TotalPages,
            Items = users
        };

        return Ok(result);
    }
}
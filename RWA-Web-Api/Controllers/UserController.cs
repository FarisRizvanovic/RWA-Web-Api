using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;


namespace RWA_Web_Api.Controllers;

[Authorize(Policy = "AdminOnly")]
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

    [HttpPut("/user/update/{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserDto updatedUser)
    {
        if (id!= updatedUser.id)
        {
            return BadRequest();
        }

        var existingUser = _userRepository.GetUserById(id);
        if (existingUser == null)
        {
            return NotFound();
        }
        
        existingUser.username = updatedUser.username;
        existingUser.first_name = updatedUser.first_name;
        existingUser.last_name = updatedUser.last_name;
        existingUser.role = updatedUser.role;

        _userRepository.UpdateUser(existingUser);

        return Ok();
    }

    [HttpDelete("/user/delete/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _userRepository.GetUserById(id);

        if (user==null)
        {
            return NotFound();
        }

        var result = _userRepository.DeleteUser(user);

        return result ? Ok() : NotFound("Something went wrong.");
    }
}
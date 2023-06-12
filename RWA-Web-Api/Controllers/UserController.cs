using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;

namespace RWA_Web_Api.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : BaseController
{
    public UserController(ApplicationDbContext dbContext, ILogger<BaseController> logger) : base(dbContext, logger)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _dbContext.Users.ToListAsync();

        return Ok(users);
    }

}
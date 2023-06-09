using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CustomersController : BaseController
{
    public CustomersController(ApplicationDbContext dbContext, ILogger<CustomersController> logger) : base(dbContext,
        logger)
    {
    }

    [HttpGet(Name = "GetCategories")]
    public async Task<IEnumerable<Category>> GetCategories()
    {
        var records = await _dbContext.Categories.ToListAsync();
        return records;
    }
}
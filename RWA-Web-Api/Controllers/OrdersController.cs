using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrdersController : BaseController
{
    public OrdersController(ApplicationDbContext dbContext, ILogger<BaseController> logger) : base(dbContext, logger)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _dbContext.Orders.ToListAsync();

        return Ok(orders);
    }
}
using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Context;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderItemsController : BaseController
{
 
    public OrderItemsController(ApplicationDbContext dbContext, ILogger<BaseController> logger) : base(dbContext, logger)
    {
    }
    
    [HttpGet("orderitems/orderid={orderId}")]
    public IActionResult GetOrderItems(int orderId)
    {
        var orderItems = _dbContext.OrderItems
            .Where(oi => oi.order_id == orderId)
            .ToList();

        return Ok(orderItems);
    }

    
}
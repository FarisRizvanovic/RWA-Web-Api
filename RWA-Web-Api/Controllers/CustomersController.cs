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

    
    // [HttpGet("orderid={orderId}")]
    // public async Task<IActionResult> GetCustomerNameForOrderId(int orderId)
    // {
    //     var order = await _dbContext.Orders
    //         .Include(o => o.customer)
    //         .FirstOrDefaultAsync(o => o.order_id == orderId);
    //
    //
    //     
    //     if (order == null)
    //     {
    //         NotFound();
    //     }
    //     
    //     return Ok(order.customer.name);
    // }
    
    [HttpGet("orderid={customerId}")]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        var customer = await _dbContext.Customers.FindAsync(customerId);
        
        if (customer == null)
        {
            return NotFound();
        }
        
        return Ok(customer);
    }
}
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrdersRepository _ordersRepository;

    public OrdersController(ILogger<OrdersController> logger, IOrdersRepository ordersRepository)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(Order))]
    [ProducesResponseType(400)]
    public  IActionResult GetOrders()
    {
        var orders = _ordersRepository.GetOrders();

        if (orders == null)
        {
            BadRequest();
        }
        
        return Ok(orders);
    }
}
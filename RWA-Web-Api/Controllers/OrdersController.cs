using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersRepository _ordersRepository;

    public OrdersController(ILogger<BaseController> logger, IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    [HttpGet]
    public  IActionResult GetOrders()
    {
        var orders = _ordersRepository.GetOrders();

        return Ok(orders);
    }
}
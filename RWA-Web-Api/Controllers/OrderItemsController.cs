using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderItemsController : ControllerBase
{
    private readonly ILogger<OrderItemsController> _logger;
    private readonly IOrderItemsRepository _orderItemsRepository;

    public OrderItemsController(ILogger<OrderItemsController> logger, IOrderItemsRepository orderItemsRepository)
    {
        _logger = logger;
        _orderItemsRepository = orderItemsRepository;
    }

    [HttpGet("orderitems/orderid={orderId}")]
    [ProducesResponseType(200, Type = typeof(OrderItem))]
    [ProducesResponseType(404)]
    public IActionResult GetOrderItems(int orderId)
    {
        var orderItems = _orderItemsRepository.GetOrderItems(orderId);

        if (orderItems == null)
        {
            return NotFound();
        }

        return Ok(orderItems);
    }
}
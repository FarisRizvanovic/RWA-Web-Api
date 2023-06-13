using AutoMapper;
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
    private readonly IMapper _mapper;

    public OrderItemsController(ILogger<OrderItemsController> logger, IOrderItemsRepository orderItemsRepository, IMapper mapper)
    {
        _logger = logger;
        _orderItemsRepository = orderItemsRepository;
        _mapper = mapper;
    }

    [HttpGet("orderitems/orderid={orderId}")]
    [ProducesResponseType(200, Type = typeof(OrderItemDto))]
    [ProducesResponseType(404)]
    public IActionResult GetOrderItems(int orderId)
    {
        var orderItems = _mapper.Map<List<OrderItemDto>>(_orderItemsRepository.GetOrderItems(orderId));

        if (orderItems == null)
        {
            return NotFound();
        }

        return Ok(orderItems);
    }
}
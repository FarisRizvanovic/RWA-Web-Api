using AutoMapper;
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
    private readonly IMapper _mapper;

    public OrdersController(ILogger<OrdersController> logger, IOrdersRepository ordersRepository, IMapper mapper)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(OrderDto))]
    [ProducesResponseType(400)]
    public  IActionResult GetOrders()
    {
        var orders = _mapper.Map<List<OrderDto>>(_ordersRepository.GetOrders());

        if (orders == null)
        {
            BadRequest();
        }
        
        return Ok(orders);
    }
}
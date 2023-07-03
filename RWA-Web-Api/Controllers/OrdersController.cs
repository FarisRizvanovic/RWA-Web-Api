using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Controllers;

[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/[controller]/[action]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;
    private readonly IOrderItemsRepository _orderItemsRepository;

    public OrdersController(ILogger<OrdersController> logger, IOrdersRepository ordersRepository, IMapper mapper,
        IOrderItemsRepository orderItemsRepository)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
        _mapper = mapper;
        _orderItemsRepository = orderItemsRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(OrderDto))]
    [ProducesResponseType(400)]
    public IActionResult GetOrders()
    {
        var orders = _mapper.Map<List<OrderDto>>(_ordersRepository.GetOrders());

        if (orders == null)
        {
            BadRequest();
        }

        return Ok(orders);
    }

    [AllowAnonymous]
    [HttpPost("/orders/add")]
    public IActionResult AddOrderWithItems(OrderRequestModel orderRequest)
    {
        
        Console.WriteLine("NOVA NARUZDBA " + orderRequest.OrderItems.Count);
        
        var order = new Order
        {
            customer_id = orderRequest.CustomerId,
            total_amount = orderRequest.TotalAmount,
            status = orderRequest.Status
        };

        if (orderRequest.OrderItems != null && orderRequest.OrderItems.Any())
        {
            foreach (var orderItem in orderRequest.OrderItems.Select(orderItemRequest => new OrderItem
                     {
                         product_id = orderItemRequest.ProductId,
                         quantity = orderItemRequest.Quantity,
                         price = orderItemRequest.Price,
                     }))
            {
                order.OrderItems.Add(orderItem);
            }
        }
        
        var request = _ordersRepository.AddOrder(order);
        
        return request ? Ok() : BadRequest();
    }

    [HttpDelete("/order/delete/{id}")]
    public IActionResult DeleteOrder(int id)
    {
        var order = _ordersRepository.GetOrderById(id);

        if (order==null)
        {
            return NotFound();
        }

        var result = _ordersRepository.DeleteOrder(order);
            
        return result ? Ok() : BadRequest("Something went wrong.");
    }

    [Authorize]
    [HttpGet("/orders/user")]
    public IActionResult GetOrdersForLoggedInUser()
    {
        var user = HttpContext.User;

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = user.FindFirst(ClaimTypes.Name)?.Value;
        var role = user.FindFirst(ClaimTypes.Role)?.Value;

        var orders = _ordersRepository.GetCustomerOrders(int.Parse(userId!));

        
        
        return Ok(orders);
    }
}
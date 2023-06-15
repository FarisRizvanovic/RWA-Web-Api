using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderItemsController : ControllerBase
{
    private readonly ILogger<OrderItemsController> _logger;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IMapper _mapper;

    public OrderItemsController(ILogger<OrderItemsController> logger, IOrderItemsRepository orderItemsRepository,
        IMapper mapper)
    {
        _logger = logger;
        _orderItemsRepository = orderItemsRepository;
        _mapper = mapper;
    }
    
    [HttpGet("orderitems/{orderId}/{page}")]
    [ProducesResponseType(200, Type = typeof(PaginationResult<OrderItemDto>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult GetOrderItems(int orderId, int page)
    {
        if (orderId <= 0 || page <= 0)
        {
            return BadRequest("Invalid orderId or page.");
        }
        
        var paginationResult = _orderItemsRepository.GetOrderItems(orderId, page);
        
        var orderItemsDtos = _mapper.Map<List<OrderItemDto>>(paginationResult.Items);

        var result = new PaginationResult<OrderItemDto>()
        {
            Page = paginationResult.Page,
            PageSize = paginationResult.PageSize,
            TotalItems = paginationResult.TotalItems,
            TotalPages = paginationResult.TotalPages,
            Items = orderItemsDtos
        };


        return Ok(result);
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Controllers;

[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/[controller]/[action]")]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository, IMapper mapper)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    [HttpGet("orderid={customerId}")]
    [ProducesResponseType(200, Type = typeof(CustomerDto))]
    [ProducesResponseType(404)]
    public IActionResult GetCustomerById(int customerId)
    {
        var customer = _mapper.Map<CustomerDto>(_customerRepository.GetCustomerById(customerId));
        Console.WriteLine("TEST " + customerId);
        if (customer == null)
        {
            return NotFound("Customer with the provided id not found.");
        }
        
        return Ok(customer);
    }
}
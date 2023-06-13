using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    [HttpGet("orderid={customerId}")]
    [ProducesResponseType(200, Type = typeof(Customer))]
    [ProducesResponseType(404)]
    public IActionResult GetCustomerById(int customerId)
    {
        var customer = _customerRepository.GetCustomerById(customerId);
        Console.WriteLine("TEST " + customerId);
        if (customer == null)
        {
            return NotFound("Customer with the provided id not found.");
        }
        
        return Ok(customer);
    }
}
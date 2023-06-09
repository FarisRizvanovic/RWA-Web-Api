using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Context;

namespace RWA_Web_Api.Controllers;

public class BaseController : ControllerBase
{
    protected readonly ApplicationDbContext _dbContext;
    protected readonly ILogger<BaseController> _logger;

    public BaseController(ApplicationDbContext dbContext, ILogger<BaseController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
}
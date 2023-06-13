using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController : BaseController
{
    public CategoryController(ApplicationDbContext dbContext, ILogger<BaseController> logger) : base(dbContext, logger)
    {
    }

    [HttpGet]
    public IActionResult GetCategoryCount()
    {
        try
        {
            var entryCount = _dbContext.Categories.Count();
            return Ok(entryCount);
        }
        catch (Exception e)
        {
            return StatusCode(500, "An error occurred while retrieving entry count.");
        }
    }
    
    [HttpGet(Name = "GetCategories")]
    public async Task<IEnumerable<Category>> GetCategories()
    {
        var records = await _dbContext.Categories.ToListAsync();
        return records;
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetCategoriesWithNumberOfProducts()
    {
        var categories =await _dbContext.Categories.Include(c => c.Products)
            .Select(c => new { c.category_id, c.name, product_count = c.Products.Count }).ToListAsync();

        if (categories == null)
        {
            NotFound();
        }

        return Ok(categories);
    }
}
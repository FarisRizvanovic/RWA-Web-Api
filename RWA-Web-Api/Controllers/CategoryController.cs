using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ILogger<CategoryController> logger, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(int))]
    public IActionResult GetCategoryCount()
    {
        var categoryCount = _categoryRepository.GetCategoryCount();
        return Ok(categoryCount);
    }

    [HttpGet(Name = "GetCategories")]
    [ProducesResponseType(200, Type = typeof(Category))]
    [ProducesResponseType(400)]
    public IActionResult GetCategories()
    {
        var categories = _categoryRepository.getCategories();

        if (categories==null)
        {
            return BadRequest();
        }
        
        return Ok(categories);
    }

    [HttpGet()]
    [ProducesResponseType(200, Type = typeof(CategoryWithItemNumber))]
    
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetCategoriesWithNumberOfProducts()
    {
        var categoriesWithItemNumber = _categoryRepository.GetCategoriesWithNumberOfProducts();

        if (categoriesWithItemNumber == null)
        {
            return BadRequest();
        }

        return Ok(categoriesWithItemNumber);
    }
}
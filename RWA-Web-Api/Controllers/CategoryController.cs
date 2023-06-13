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
    private readonly ILogger<BaseController> _logger;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ILogger<BaseController> logger, ICategoryRepository categoryRepository)
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
    public IEnumerable<Category> GetCategories()
    {
        var categories = _categoryRepository.getCategories();

        if (categories==null)
        {
            
        }
        
        return categories;
    }

    [HttpGet()]
    [ProducesResponseType(200, Type = typeof(CategoryWithItemNumber))]
    public async Task<IActionResult> GetCategoriesWithNumberOfProducts()
    {
        var categoriesWithItemNumber = _categoryRepository.GetCategoriesWithNumberOfProducts();

        if (categoriesWithItemNumber == null)
        {
            NotFound("No categories");
        }

        return Ok(categoriesWithItemNumber);
    }
}
using AutoMapper;
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
    private readonly IMapper _mapper;

    public CategoryController(ILogger<CategoryController> logger, ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet("/categories/count")]
    [ProducesResponseType(200, Type = typeof(int))]
    public IActionResult GetCategoryCount()
    {
        var categoryCount = _categoryRepository.GetCategoryCount();
        return Ok(categoryCount);
    }

    [HttpGet("/categories")]
    [ProducesResponseType(200, Type = typeof(CategoryDto))]
    [ProducesResponseType(400)]
    public IActionResult GetAllCategories()
    {
        var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetAllCategories());

        if (categories == null)
        {
            return BadRequest();
        }

        return Ok(categories);
    }

    [HttpGet("/categories/itemcount/{page}/{searchTerm?}")]
    [ProducesResponseType(200, Type = typeof(PaginationResult<CategoryWithItemNumber>))]
    [ProducesResponseType(400)]
    public IActionResult GetCategoriesWithNumberOfProducts(int page, string? searchTerm = "")
    {
        if (page<0)
        {
            return BadRequest("Invalid page.");
        }
        
        var categoriesWithItemNumber = _categoryRepository.GetCategoriesWithNumberOfProducts(page, searchTerm);
    
        return Ok(categoriesWithItemNumber);
    }


    [HttpGet("/categories/{page}/{searchTerm?}")]
    [ProducesResponseType(200, Type = typeof(PaginationResult<CategoryDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetCategories(int page, string? searchTerm)
    {
        if (page <= 0)
        {
            return BadRequest("Invalid page.");
        }

        var paginationResult = _categoryRepository.GetCategories(page, searchTerm);

        var categoriesDtos = _mapper.Map<List<CategoryDto>>(paginationResult.Items);

        if (categoriesDtos == null)
        {
            return BadRequest();
        }

        var result = new PaginationResult<CategoryDto>()
        {
            Page = paginationResult.Page,
            PageSize = paginationResult.PageSize,
            TotalItems = paginationResult.TotalItems,
            TotalPages = paginationResult.TotalPages,
            Items = categoriesDtos
        };

        return Ok(result);
    }
}
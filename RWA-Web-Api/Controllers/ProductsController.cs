using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;
using RWA_Web_Api.Util;

namespace RWA_Web_Api.Controllers;

[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository,
        ICategoryRepository categoryRepository, IMapper
            mapper)
    {
        _logger = logger;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /**
     * Gets All products
     */
    [AllowAnonymous]
    [HttpGet("/products/{page}/{searchTerm?}")]
    [ProducesResponseType(200, Type = typeof(PaginationResult<ProductDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetProducts(int page, string? searchTerm = "")
    {
        if (page <= 0)
        {
            return BadRequest("Invalid page number");
        }

        var paginationResponse = _productRepository.GetProducts(page, searchTerm);

        if (page > paginationResponse.TotalPages && paginationResponse.TotalPages != 0)
        {
            return NotFound();
        }

        var products = _mapper.Map<List<ProductDto>>(paginationResponse.Items);

        var result = new PaginationResult<ProductDto>()
        {
            Page = paginationResponse.Page,
            PageSize = paginationResponse.PageSize,
            TotalItems = paginationResponse.TotalItems,
            TotalPages = paginationResponse.TotalPages,
            Items = products
        };

        return Ok(result);
    }

    /**
     * Gets All products by categoryId
     */
    [AllowAnonymous]
    [HttpGet("/products/bycategory/{categoryId}/{page}/{searchTerm?}")]
    [ProducesResponseType(200, Type = typeof(PaginationResult<ProductDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetProducts(int categoryId, int page, string? searchTerm = "")
    {
        if (page <= 0)
        {
            return BadRequest("Invalid page number");
        }

        bool categoryExists = _categoryRepository.DoesACategoryExist(categoryId);

        if (!categoryExists)
        {
            return NotFound();
        }

        var paginationResponse = _productRepository.GetProductsByCategory(page, categoryId, searchTerm);

        if (page > paginationResponse.TotalPages && paginationResponse.TotalPages != 0)
        {
            return NotFound();
        }

        var products = _mapper.Map<List<ProductDto>>(paginationResponse.Items);

        var result = new PaginationResult<ProductDto>()
        {
            Page = paginationResponse.Page,
            PageSize = paginationResponse.PageSize,
            TotalItems = paginationResponse.TotalItems,
            TotalPages = paginationResponse.TotalPages,
            Items = products
        };

        return Ok(result);
    }

   
    /**
     * Updates the image of a product with a given id
     */
    [HttpPut("/product/update/image/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateProductImage(int id, [FromForm] IFormFile? imageFile)
    {
        try
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound("User with given id not found.");
            }

            if (imageFile == null)
            {
                return BadRequest("Image not found.");
            }

            // Check if the uploaded file exists and is an image
            if (imageFile != null && imageFile.Length > 0 && FileUtils.IsImage(imageFile))
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine("images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyToAsync(stream);
                }

                _productRepository.UpdateProductImage(filePath, id);
            }
            else
            {
                return BadRequest("Invalid file or file format");
            }

            return Ok(product.title);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update product image");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error occurred while updating product image");
        }
    }

   
    /**
     * Adds a new product with image
     */
    [HttpPost("/products")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public IActionResult AddProduct([FromForm] ProductWithImage productWithImage)
    {
        // Extract the product details
        var product = new Product
        {
            title = productWithImage.Title,
            isNew = productWithImage.IsNew,
            oldPrice = productWithImage.OldPrice,
            price = productWithImage.Price,
            stock = productWithImage.Stock,
            description = productWithImage.Description,
            category_id = productWithImage.CategoryId,
            rating = productWithImage.Rating
        };

        var file = productWithImage.ImageFile;

        // Handle the image file
        try
        {
            // Check if the uploaded file exists and is an image
            if (file != null && file.Length > 0 && FileUtils.IsImage(file))
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }

                product.image = filePath;
            }
            else
            {
                return BadRequest("Invalid file or file format");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update product image");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error occurred while updating product image");
        }

        // Save the product to the database
        _productRepository.AddProduct(product);

        return Ok("Product added successfully");
    }

    /**
     * Get number of products
     */
    [HttpGet("/products/count")]
    [ProducesResponseType(200, Type = typeof(int))]
    public IActionResult GetProductCount()
    {
        return Ok(_productRepository.GetProductCount());
    }

    /**
     * Get all products that are low on stock
     */
    [HttpGet("/products/lowonstock/{limit}/{page}/{searchTerm?}")]
    [ProducesResponseType(200, Type = typeof(PaginationResult<ProductDto>))]
    public IActionResult GetProductsLowOnStock(int limit, int page, string? searchTerm)
    {
        if (page <= 0)
        {
            return BadRequest("Invalid page number");
        }

        var paginationResult = _productRepository.GetProductsLowOnStock(limit, page, searchTerm);

        if (page > paginationResult.TotalPages && paginationResult.TotalPages != 0)
        {
            return NotFound();
        }

        var products = _mapper.Map<List<ProductDto>>(paginationResult.Items);

        var result = new PaginationResult<ProductDto>()
        {
            Page = paginationResult.Page,
            PageSize = paginationResult.PageSize,
            TotalItems = paginationResult.TotalItems,
            TotalPages = paginationResult.TotalPages,
            Items = products
        };

        return Ok(result);
    }

    /**
     * Get number of products low on stock
     */
    [HttpGet("/products/lowonstock/count/{limit}")]
    [ProducesResponseType(200, Type = typeof(int))]
    public IActionResult GetProductsLowOnStockCount(int limit)
    {
        var count = _productRepository.GetNumberOfProductLowOnStock(limit);
        return Ok(count);
    }

    /**
     * Gets product by Id
     */
    [AllowAnonymous]
    [HttpGet("/product/{productId}")]
    [ProducesResponseType(200, Type = typeof(ProductDto))]
    [ProducesResponseType(404)]
    public IActionResult GetProductById(int productId)
    {
        var product = _mapper.Map<ProductDto>(_productRepository.GetProductById(productId));

        if (product == null)
        {
            NotFound("Product with provided id not found.");
        }

        return Ok(product);
    }
    
    [HttpPut("/product/update/{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] ProductDto updatedProduct)
    {
        if (id != updatedProduct.product_id)
        {
            return BadRequest();
        }

        var existingProduct = _productRepository.GetProductById(id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.product_id = updatedProduct.product_id;
        existingProduct.category_id = updatedProduct.category_id;
        existingProduct.title = updatedProduct.title;
        existingProduct.isNew = updatedProduct.isNew;
        existingProduct.description = updatedProduct.description;
        existingProduct.rating = updatedProduct.rating;
        existingProduct.stock = updatedProduct.stock;
        existingProduct.oldPrice = updatedProduct.oldPrice;
        existingProduct.price = updatedProduct.price;

        _productRepository.UpdateProduct(existingProduct);

        return Ok();
    }
    
    [HttpDelete("/product/delete/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _productRepository.GetProductById(id);
        if (product== null)
        {
            return NotFound();
        }

        var result = _productRepository.DeleteProduct(product);
        
        return result ? Ok() : BadRequest("Something went wrong.");
    }
    
}
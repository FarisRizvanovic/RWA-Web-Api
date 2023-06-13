using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;
using RWA_Web_Api.Util;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductRepository _productRepository;

    public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    /**
     * Gets All products
     * TODO: ADD PAGINATION
     */
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
    [ProducesResponseType(400)]
    public IActionResult getProducts()
    {
        var products = _productRepository.GetProducts();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(products);
    }

    /**
     * Updates the image of a product with a given id
     */
    [HttpPut("{id}/image")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateProductImage(int id, [FromForm] ImageFile? imageFile)
    {
        try
        {
            var product = _productRepository.GetProductById(id);
            var file = imageFile.file;
            if (product == null)
            {
                return NotFound("User with given id not found.");
            }

            if (file == null)
            {
                return BadRequest("Image not found.");
            }

            // Check if the uploaded file exists and is an image
            if (file != null && file.Length > 0 && FileUtils.IsImage(file))
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }

                _productRepository.UpdateProductImage(filePath, id);
            }
            else
            {
                return BadRequest("Invalid file or file format");
            }

            return Ok();
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
    [HttpPost]
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
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(int))]
    public IActionResult GetProductCount()
    {
        return Ok(_productRepository.GetProductCount());
    }

    /**
     * Get all products that are low on stock
     */
    [HttpGet("products/lowonstock/limit={limit}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
    public IActionResult GetProductsLowOnStock(int limit)
    {
        var products = _productRepository.GetProductsLowOnStock(limit);
        return Ok(products);
    }

    /**
     * Get number of products low on stock
     */
    [HttpGet("products/lowonstockcount/limit={limit}")]
    [ProducesResponseType(200, Type = typeof(int))]
    public IActionResult GetProductsLowOnStockCount(int limit)
    {
        var count = _productRepository.GetNumberOfProductLowOnStock(limit);
        return Ok(count);
    }

    /**
     * Gets product by Id
     */
    [HttpGet("product/id={productId}")]
    [ProducesResponseType(200, Type = typeof(Product))]
    [ProducesResponseType(404)]
    public IActionResult GetProductById(int productId)
    {
        var product = _productRepository.GetProductById(productId);

        if (product == null)
        {
            NotFound("Product with provided id not found.");
        }

        return Ok(product);
    }
    
}
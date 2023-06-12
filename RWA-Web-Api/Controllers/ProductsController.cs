using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductsController : BaseController
{
    public ProductsController(ApplicationDbContext dbContext, ILogger<ProductsController> logger) : base(dbContext,
        logger)
    {
    }

    /**
     * Gets All products
     * TODO: ADD PAGINATION
     */
    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        var records = await _dbContext.Products.ToListAsync();
        return Ok(records);
    }

    /*
    // TO SAVE DATA AS BLOBS IN MYSQL
    // // PUT: api/products/1/image
    // [HttpPut("{id}/image")]
    // public async Task<IActionResult> UpdateProductImage(int id, [FromForm] IFormFile file)
    // {
    //     try
    //     {
    //         var product = await _dbContext.Products.FindAsync(id);
    //         if (product == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         // Check if the uploaded file exists and is an image
    //         if (file != null && file.Length > 0 && IsImage(file))
    //         {
    //             using (var memoryStream = new MemoryStream())
    //             {
    //                 await file.CopyToAsync(memoryStream);
    //                 
    //                 var imageBytes = memoryStream.ToArray();
    //
    //                 product.image = imageBytes;
    //                 await _dbContext.SaveChangesAsync();
    //             }
    //         }
    //         else
    //         {
    //             return BadRequest("Invalid file or file format");
    //         }
    //
    //         return NoContent();
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Failed to update product image");
    //         return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating product image");
    //     }
    // }
    */

    /**
     * Checks if the provided file is an image
     */
    private static bool IsImage(IFormFile file)
    {
        if (file.ContentType.ToLower() != "image/jpg" &&
            file.ContentType.ToLower() != "image/jpeg" &&
            file.ContentType.ToLower() != "image/pjpeg" &&
            file.ContentType.ToLower() != "image/gif" &&
            file.ContentType.ToLower() != "image/x-png" &&
            file.ContentType.ToLower() != "image/png")
        {
            return false;
        }

        return true;
    }

    /**
     * Updates the image of a product with a given id
     */
    [HttpPut("{id}/image")]
    public async Task<IActionResult> UpdateProductImage(int id, [FromForm] IFormFile? file)
    {
        try
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Check if the uploaded file exists and is an image
            if (file != null && file.Length > 0 && IsImage(file))
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("images", fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                product.image = filePath;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Invalid file or file format");
            }

            return NoContent();
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
    public async Task<IActionResult> AddProduct([FromForm] ProductWithImage productWithImage)
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
            if (file != null && file.Length > 0 && IsImage(file))
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("images", fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
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
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    /**
     * Get number of products
     */
    [HttpGet]
    public IActionResult GetProductCount()
    {
        try
        {
            var entryCount = _dbContext.Products.Count();
            return Ok(entryCount);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving entry count.");
        }
    }

    /**
     * Get all products that are low on stock
     */
    [HttpGet("products/lowonstock/limit={limit}")]
    public IActionResult GetProductsLowOnStock(int limit)
    {
        var products = _dbContext.Products.Where(p => p.stock < limit).ToList();
        return Ok(products);
    }

    /**
     * Get number of products low on stock
     */
    [HttpGet("products/lowonstockcount/limit={limit}")]
    public IActionResult GetProductsLowOnStockCount(int limit)
    {
        var products = _dbContext.Products.Where(p => p.stock < limit).ToList().Count;
        return Ok(products);
    }

    [HttpGet("product/id={productId}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        var product = await _dbContext.Products.FindAsync(productId);

        if (product==null)
        {
            NotFound();
        }
        Console.WriteLine(product.product_id);
        
        return Ok(product);
    }

}
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

    [HttpGet(Name = "GetProducts")]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        var records = await _dbContext.Products.ToListAsync();
        return records;
    }


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
    //
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

        Console.WriteLine(product.image);
        // Save the product to the database
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        

        return Ok();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<Product> GetProducts()
    {
        return _dbContext.Products.OrderBy(p => p.product_id).ToList();
    }

    public Product? GetProductById(int productId)
    {
        return _dbContext.Products.Find(productId);
    }

    public void UpdateProductImage(string imageUri, int productId)
    {
        var product =  _dbContext.Products.Find(productId);
        product.image = imageUri; 
        _dbContext.SaveChangesAsync();
    }

    public void AddProduct(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChangesAsync();
    }

    public int GetProductCount()
    {
        return _dbContext.Products.Count();
    }

    public ICollection<Product> GetProductsLowOnStock(int limit)
    {
        return _dbContext.Products.Where(p => p.stock < limit).ToList();
    }

    public int GetNumberOfProductLowOnStock(int limit)
    {
        return GetProductsLowOnStock(limit).Count();
    }
}
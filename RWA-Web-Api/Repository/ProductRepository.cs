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
    readonly int _pageSize = 10;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public PaginationResult<Product> GetProducts(int page, string? searchTerm)
    {
        var query = _dbContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.title.Contains(searchTerm));
        }

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)_pageSize);

        var products = query
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        return new PaginationResult<Product>()
        {
            Page = page,
            PageSize = _pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Items = products
        };
    }

    public PaginationResult<Product> GetProductsByCategory(int page, int categoryId, string? searchTerm)
    {
        var query = _dbContext.Products
            .Where(p => p.category_id == categoryId);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.title.Contains(searchTerm));
        }

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)_pageSize);

        var products = query
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        return new PaginationResult<Product>()
        {
            Page = page,
            PageSize = _pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Items = products
        };
    }

    public Product? GetProductById(int productId)
    {
        return _dbContext.Products.Find(productId);
    }

    public void UpdateProduct(Product product)
    {
        _dbContext.Entry(product).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void UpdateProductImage(string imageUri, int productId)
    {
        var product = _dbContext.Products.Find(productId);
        if (product == null) return;
        product.image = imageUri;
        _dbContext.SaveChanges();

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

    public PaginationResult<Product> GetProductsLowOnStock(int limit, int page, string? searchTerm)
    {
        var query = _dbContext.Products.Where(p => p.stock < limit);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.title.Contains(searchTerm));
        }

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)_pageSize);

        var products = query
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        return new PaginationResult<Product>()
        {
            Page = page,
            PageSize = _pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Items = products
        };
    }

    public int GetNumberOfProductLowOnStock(int limit)
    {
        return _dbContext.Products.Count(p => p.stock < limit);
    }

    public bool DeleteProduct(Product product)
    {
         _dbContext.Products.Remove(product);
         var affectedRows = _dbContext.SaveChanges();
         
         return affectedRows != 0;
    }
}
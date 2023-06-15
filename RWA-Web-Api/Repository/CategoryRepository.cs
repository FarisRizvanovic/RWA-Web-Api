using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    private int pageSize = 5;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _dbContext.Categories.ToList();
    }

    public PaginationResult<Category> GetCategories(int page, string searchTerm)
    {
        var query = _dbContext.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(c => c.name.Contains(searchTerm));
        }

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var categories = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginationResult<Category>()
        {
            Page = page,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Items = categories
        };
    }

    public int GetCategoryCount()
    {
        return _dbContext.Categories.Count();
    }

    public PaginationResult<CategoryWithItemNumber> GetCategoriesWithNumberOfProducts(int page, string searchTerm)
    {
        var query = _dbContext.Categories.Include(c => c.Products)
            .Select(c => new CategoryWithItemNumber(c.category_id, c.name, c.Products.Count));

        if (string.IsNullOrEmpty(searchTerm))
        {
            query = _dbContext.Categories.Include(c => c.Products)
                .Where(c => c.name.Contains(searchTerm))
                .Select(c => new CategoryWithItemNumber(c.category_id, c.name, c.Products.Count));
            
        }

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var categoriesWithItemNumber = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginationResult<CategoryWithItemNumber>()
        {
            Page = page,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Items = categoriesWithItemNumber
        };
    }
}
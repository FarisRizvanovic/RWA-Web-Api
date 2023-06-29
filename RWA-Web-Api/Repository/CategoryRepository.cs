using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly int _pageSize = 5;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool AddCategory(string name, string description)
    {
        var query = _dbContext.Categories.Count(c => c.name == name);
        if (query > 0)
        {
            return false;
        }

        _dbContext.Categories.Add(new Category() { name = name, description = description });
        var affectedRows = _dbContext.SaveChanges();

        return affectedRows != 0;
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
        int totalPages = (int)Math.Ceiling(totalItems / (double)_pageSize);

        var categories = query
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        return new PaginationResult<Category>()
        {
            Page = page,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Items = categories
        };
    }

    public Category? GetCategoryById(int id)
    {
        return _dbContext.Categories.Find(id);
    }

    public void UpdateCategory(Category category)
    {
        _dbContext.Categories.Entry(category).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public int GetCategoryCount()
    {
        return _dbContext.Categories.Count();
    }

    public PaginationResult<CategoryWithItemNumber> GetCategoriesWithNumberOfProducts(int page, string searchTerm)
    {
        var query = _dbContext.Categories.Include(c => c.Products)
            .Select(c => new CategoryWithItemNumber()
            {
                category_id = c.category_id,
                name = c.name,
                product_count = c.Products.Count,
                description = c.description
            });

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = _dbContext.Categories.Include(c => c.Products)
                .Where(c => c.name.Contains(searchTerm))
                .Select(c => new CategoryWithItemNumber()
                {
                    category_id = c.category_id,
                    name = c.name,
                    product_count = c.Products.Count,
                    description = c.description
                });
        }

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)_pageSize);

        var categoriesWithItemNumber = query
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        return new PaginationResult<CategoryWithItemNumber>()
        {
            Page = page,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Items = categoriesWithItemNumber
        };
    }

    public bool DoesACategoryExist(int categoryId)
    {
        return _dbContext.Categories.Any(c => c.category_id == categoryId);
    }

    public bool DeleteCategory(Category category)
    {
        _dbContext.Categories.Remove(category);
        var affectedRows = _dbContext.SaveChanges();

        return affectedRows != 0;
    }
}
using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<Category> getCategories()
    {
        return _dbContext.Categories.ToList();
    }

    public int GetCategoryCount()
    {
        return _dbContext.Categories.Count();
    }

    public IEnumerable<CategoryWithItemNumber> GetCategoriesWithNumberOfProducts()
    {
        var categories = _dbContext.Categories.Include(c => c.Products)
            .Select(c => new CategoryWithItemNumber(c.category_id, c.name, c.Products.Count)).ToList();

        return categories;
    }
}
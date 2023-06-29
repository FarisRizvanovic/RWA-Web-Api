using Microsoft.VisualBasic;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Interfaces;

public interface ICategoryRepository
{
    bool AddCategory(string name, string description);
    IEnumerable<Category> GetAllCategories();

    PaginationResult<Category> GetCategories(int page, string searchTerm);

    Category? GetCategoryById(int id);

    void UpdateCategory(Category category);

    int GetCategoryCount();

    PaginationResult<CategoryWithItemNumber> GetCategoriesWithNumberOfProducts(int page, string searchTerm);

    bool DoesACategoryExist(int categoryId);

    bool DeleteCategory(Category category);
}
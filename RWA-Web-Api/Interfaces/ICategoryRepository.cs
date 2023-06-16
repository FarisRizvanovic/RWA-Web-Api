using Microsoft.VisualBasic;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAllCategories();

    PaginationResult<Category> GetCategories(int page, string searchTerm);

    int GetCategoryCount();

    PaginationResult<CategoryWithItemNumber> GetCategoriesWithNumberOfProducts(int page, string searchTerm);

    bool DoesACategoryExist(int categoryId);
}
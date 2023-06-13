using Microsoft.VisualBasic;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Interfaces;

public interface ICategoryRepository
{
    ICollection<Category> getCategories();

    int GetCategoryCount();

    IEnumerable<CategoryWithItemNumber> GetCategoriesWithNumberOfProducts();
}
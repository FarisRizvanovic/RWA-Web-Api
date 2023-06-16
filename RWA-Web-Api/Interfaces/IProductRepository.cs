using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Interfaces;

public interface IProductRepository
{
    PaginationResult<Product> GetProducts(int page, string? searchTerm);

    PaginationResult<Product> GetProductsByCategory(int page, int categoryId, string? searchTerm);

    Product? GetProductById(int productId);
    
    void UpdateProductImage(string imageUri, int productId);

    void AddProduct(Product product);

    int GetProductCount();

    PaginationResult<Product> GetProductsLowOnStock(int limit, int page, string? searchTerm);

    int GetNumberOfProductLowOnStock(int limit);

}
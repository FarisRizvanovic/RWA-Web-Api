using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Interfaces;

public interface IProductRepository
{
    ICollection<Product> GetProducts();

    Product? GetProductById(int productId);
    
    void UpdateProductImage(string imageUri, int productId);

    void AddProduct(Product product);

    int GetProductCount();

    ICollection<Product> GetProductsLowOnStock(int limit);

    int GetNumberOfProductLowOnStock(int limit);
    
}
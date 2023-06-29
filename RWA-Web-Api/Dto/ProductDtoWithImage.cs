namespace RWA_Web_Api.Models;

public class ProductDtoWithImage
{
    public int product_id { get; set; }
    public int? category_id { get; set; }
    public string title { get; set; } 
    public bool? isNew { get; set; }
    public string? description { get; set; }
    public decimal? rating { get; set; }
    public int? stock { get; set; }
    public decimal? oldPrice { get; set; }
    public decimal? price { get; set; }
    public IFormFile? imageFile { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
}
namespace RWA_Web_Api.Models;

public class ProductDto
{
    
    public int product_id { get; set; }
    public int? category_id { get; set; }
    public string title { get; set; } = null!;
    public bool? isNew { get; set; }
    public string? description { get; set; }
    public decimal? rating { get; set; }
    public int? stock { get; set; }
    public decimal? oldPrice { get; set; }
    public decimal? price { get; set; }
    public string? image { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    
}

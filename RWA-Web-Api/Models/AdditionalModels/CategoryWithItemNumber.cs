namespace RWA_Web_Api.Models.AdditionalModels;

public class CategoryWithItemNumber
{
    
    public int category_id { get; set; }
    public string name { get; set; } = null!;
    public int product_count { get; set; }
    public string? description { get; set; }
    
}
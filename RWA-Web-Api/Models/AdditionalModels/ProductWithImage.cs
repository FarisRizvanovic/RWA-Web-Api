namespace RWA_Web_Api.Models.AdditionalModels;

public class ProductWithImage
{
    public string Title { get; set; }
    public bool IsNew { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal? Price { get; set; }
    public string Description { get; set; }
    public int? CategoryId { get; set; }
    public IFormFile? ImageFile { get; set; }
    public decimal? Rating { get; set; }
}
namespace RWA_Web_Api.Models;

public class WebsiteContentDto
{
    public int content_id { get; set; }
    public string? cover_image { get; set; }
    public string? home_description { get; set; }
    public string? phone_number { get; set; }
    public string? location { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
}

namespace RWA_Web_Api.Models;

public class NewsletterDto
{
    public int email_id { get; set; }
    public string? email { get; set; }
    public DateTime? created_at { get; set; }
}
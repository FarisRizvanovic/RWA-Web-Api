namespace RWA_Web_Api.Models;

public class CustomerDto
{
    public int customer_id { get; set; }
    public string? name { get; set; }
    public string? phone_number { get; set; }
    public string? address { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    
}

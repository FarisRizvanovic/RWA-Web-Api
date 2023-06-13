namespace RWA_Web_Api.Models;

public class OrderDto
{
    public int order_id { get; set; }
    public int? customer_id { get; set; }
    public decimal? total_amount { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    
}

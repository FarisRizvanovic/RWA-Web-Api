namespace RWA_Web_Api.Models;

public class OrderItemDto
{
    public int order_item_id { get; set; }
    public int? order_id { get; set; }
    public int? product_id { get; set; }
    public int? quantity { get; set; }
    public decimal? price { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }

}

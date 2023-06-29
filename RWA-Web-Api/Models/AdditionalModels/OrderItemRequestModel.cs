namespace RWA_Web_Api.Models.AdditionalModels;

public class OrderItemRequestModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
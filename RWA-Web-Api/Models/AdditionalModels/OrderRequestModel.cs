namespace RWA_Web_Api.Models.AdditionalModels;

public class OrderRequestModel
{
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public List<OrderItemRequestModel>? OrderItems { get; set; }
}
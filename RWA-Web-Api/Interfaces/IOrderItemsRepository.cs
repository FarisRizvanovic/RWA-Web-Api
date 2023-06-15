using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Interfaces;

public interface IOrderItemsRepository
{
    PaginationResult<OrderItem> GetOrderItems(int orderId, int page);
}
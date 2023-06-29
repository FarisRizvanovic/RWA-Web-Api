using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Interfaces;

public interface IOrdersRepository
{
    ICollection<Order> GetOrders();

    bool AddOrder(Order order);

    Order? GetOrderById(int id);

    bool DeleteOrder(Order order);
}
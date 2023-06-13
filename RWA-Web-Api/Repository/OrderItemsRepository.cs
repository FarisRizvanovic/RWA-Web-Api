using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Repository;

public class OrderItemsRepository : IOrderItemsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderItemsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<OrderItem> GetOrderItems(int orderId)
    {
        return _dbContext.OrderItems
            .Where(oi => oi.order_id == orderId)
            .ToList();
    }
}
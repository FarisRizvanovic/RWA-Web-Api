using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Repository;

public class OrdersRepository : IOrdersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrdersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<Order> GetOrders()
    {
        return _dbContext.Orders.ToList();
    }

    public bool AddOrder(Order order)
    {
        _dbContext.Orders.Add(order);
        var affectedRow = _dbContext.SaveChanges();
        return affectedRow != 0;
    }

    public Order? GetOrderById(int id)
    {
        return _dbContext.Orders.Find(id);
    }

    public bool DeleteOrder(Order order)
    {
        var orderItemsToRemove = _dbContext.OrderItems.Where(oi => oi.order_id == order.order_id);
        _dbContext.OrderItems.RemoveRange(orderItemsToRemove);
        _dbContext.Orders.Remove(order);
        var affectedRows = _dbContext.SaveChanges();

        return affectedRows != 0;
    }
}
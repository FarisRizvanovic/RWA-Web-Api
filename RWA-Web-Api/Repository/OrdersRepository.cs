using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;

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
}
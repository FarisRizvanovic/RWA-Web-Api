using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Repository;

public class OrderItemsRepository : IOrderItemsRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly int _pageSize = 5;

    public OrderItemsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public PaginationResult<OrderItem> GetOrderItems(int orderId, int page)
    {
        var query = _dbContext.OrderItems.Where(
            oi => oi.order_id == orderId);

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)_pageSize);

        var orderItems = query
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        return new PaginationResult<OrderItem>
        {
            Page = page,
            PageSize = _pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Items = orderItems
        };
    }

    public void AddOrderItem(OrderItemRequestModel orderItemRequest)
    {
        _dbContext.OrderItems.Add(new OrderItem()
        {
            product_id = orderItemRequest.ProductId,
            quantity = orderItemRequest.Quantity,
            price = orderItemRequest.Price
        });

        _dbContext.SaveChanges();
    }

    public bool DeleteOrderItem(OrderItem orderItem)
    {
        _dbContext.OrderItems.Remove(orderItem);
        var affectedRows = _dbContext.SaveChanges();

        return affectedRows != 0;
    }

    public OrderItem? GetOrderItemById(int id)
    {
        return _dbContext.OrderItems.Find(id);
    }
}
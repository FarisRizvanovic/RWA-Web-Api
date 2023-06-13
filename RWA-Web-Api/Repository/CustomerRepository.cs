using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Customer? GetCustomerById(int customerID)
    {
        return _dbContext.Customers.Find(customerID);
    }
}
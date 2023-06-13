using RWA_Web_Api.Models;

namespace RWA_Web_Api.Interfaces;

public interface ICustomerRepository
{
    Customer? GetCustomerById(int customerID);

}
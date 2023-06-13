using RWA_Web_Api.Models;

namespace RWA_Web_Api.Interfaces;

public interface IUserRepository
{
    ICollection<User> GetUsers();
}
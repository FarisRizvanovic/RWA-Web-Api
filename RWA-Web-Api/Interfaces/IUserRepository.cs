using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Interfaces;

public interface IUserRepository
{
    PaginationResult<User> GetUsers(int page, string? searchTerm);
    
}
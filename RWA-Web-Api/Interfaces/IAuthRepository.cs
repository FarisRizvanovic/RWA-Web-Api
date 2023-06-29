using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Interfaces;

public interface IAuthRepository
{
    ServiceResponse<int> Register(User user, string password);

    ServiceResponse<string> Login(string username, string password);

    bool UserExists(string username);
}
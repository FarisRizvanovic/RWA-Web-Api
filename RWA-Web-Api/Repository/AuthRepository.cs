using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;
using RWA_Web_Api.Util;

namespace RWA_Web_Api.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public AuthRepository(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }
    
    public ServiceResponse<int> Register(User user, string password)
    {
        var response = new ServiceResponse<int>();
        if (UserExists(user.username))
        {
            response.Success = false;
            response.Message = "User already exists";
            return response;
        }
        AuthUtil.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        user.password_hash = passwordHash;
        user.password_salt = passwordSalt;

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        
        response.Data = user.id;
        response.Message = "User created";

        return response;
    }

    public ServiceResponse<string> Login(string username, string password)
    {
        var response = new ServiceResponse<string>();
        var user = _dbContext.Users.FirstOrDefault(u => u.username.Equals(username));

        var isPasswordHashCorrect = AuthUtil.VerifyPasswordHash(password, user.password_hash, user.password_salt);
        
        if (user == null || !isPasswordHashCorrect)
        {
            response.Success = false;
            response.Message = "Username or password is incorrect.";
        }
        else
        {
            response.Data = AuthUtil.CreateToken(user, _configuration);
        }

        return response;
    }

    public bool UserExists(string username)
    {
        return _dbContext.Users.Any(u=>u.username == username);
    }
}
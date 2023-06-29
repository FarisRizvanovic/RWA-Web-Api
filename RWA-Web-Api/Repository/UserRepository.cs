using Microsoft.EntityFrameworkCore;
using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;
using RWA_Web_Api.Models.AdditionalModels;

namespace RWA_Web_Api.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly int _pageSize = 5;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ICollection<User> GetUsers()
    {
        return _dbContext.Users.ToList();
    }

    public PaginationResult<User> GetUsers(int page, string? searchTerm)
    {
        var query = _dbContext.Users.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.first_name.Contains(searchTerm) || u.last_name.Contains(searchTerm));
        }

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)_pageSize);

        var users = query
            .Skip((page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        return new PaginationResult<User>()
        {
            Page = page,
            PageSize = _pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Items = users
        };
    }

    public User? GetUserById(int id)
    {
        return _dbContext.Users.Find(id);
    }

    public void UpdateUser(User user)
    {
        _dbContext.Entry(user).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public bool DeleteUser(User user)
    {
        _dbContext.Remove(user);
        var affectedRows = _dbContext.SaveChanges();

        return affectedRows != 0;
    }
}
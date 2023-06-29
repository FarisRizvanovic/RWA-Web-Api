using RWA_Web_Api.Context;
using RWA_Web_Api.Interfaces;
using RWA_Web_Api.Models;

namespace RWA_Web_Api.Repository;

public class NewsletterRepository : INewsLetterRepository
{
    private readonly ApplicationDbContext _dbContext;

    public NewsletterRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool AddSubscription(string email)
    {
        var query = _dbContext.Newsletters.Count(n => n.email == email);

        if (query > 0)
        {
            return false;
        }
        
        _dbContext.Newsletters.Add(new Newsletter() { email = email });
        var affectedRows = _dbContext.SaveChanges();

        return affectedRows != 0;
    }
}
namespace RWA_Web_Api.Interfaces;

public interface INewsLetterRepository
{
    bool AddSubscription(string email);
}
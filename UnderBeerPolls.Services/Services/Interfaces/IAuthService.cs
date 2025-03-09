namespace UnderBeerPolls.Services.Services.Interfaces;

public interface IAuthService
{
    Task<bool> Register(string username, string password);
    
    Task<string> Login(string username, string password);
}
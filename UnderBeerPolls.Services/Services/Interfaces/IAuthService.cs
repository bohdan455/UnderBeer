namespace UnderBeerPolls.Services.Services.Interfaces;

public interface IAuthService
{
    Task Register(string username, string password);
    
    Task<string> Login(string username, string password);
}
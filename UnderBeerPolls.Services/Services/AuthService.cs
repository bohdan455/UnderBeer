using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UnderBeerPolls.DataLayer;
using UnderBeerPolls.DataLayer.DbModels;
using UnderBeerPolls.Services.Services.Interfaces;

namespace UnderBeerPolls.Services.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public AuthService(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    
    public async Task<bool> Register(string username, string password)
    {
        if (await _context.Users.AnyAsync(x => x.Username == username))
        {
            return false;
        }
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        _context.Add(new User(username, hashedPassword));
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string> Login(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null)
        {
            return string.Empty;
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return string.Empty;
        }

        return GenerateJwtToken(username);
    }
    
    private string GenerateJwtToken(string username)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, username),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings["ExpiryMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
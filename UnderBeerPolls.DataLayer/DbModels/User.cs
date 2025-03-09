using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UnderBeerPolls.DataLayer.DbModels;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    public User()
    {
        
    }
    
    public User(string username, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
    }
    
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; }

    public List<Poll> Polls { get; set; } = [];
}
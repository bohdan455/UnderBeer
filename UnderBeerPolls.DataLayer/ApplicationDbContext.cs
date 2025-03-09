using Microsoft.EntityFrameworkCore;
using UnderBeerPolls.DataLayer.DbModels;

namespace UnderBeerPolls.DataLayer;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public virtual DbSet<Poll> Polls { get; set; }
    
    public virtual DbSet<PollOption> PollOptions { get; set; }
    
    public virtual DbSet<PollOptionResponse> PollOptionResponses { get; set; }
    
    public virtual DbSet<PollResponse> PollResponses { get; set; }
}
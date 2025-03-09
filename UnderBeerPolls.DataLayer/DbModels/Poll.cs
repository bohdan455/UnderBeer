using System.ComponentModel.DataAnnotations;

namespace UnderBeerPolls.DataLayer.DbModels;

public class Poll
{
    public Guid Id { get; set; }
    
    [MaxLength(3000)]
    public string Title { get; set; }

    public List<PollOption> Options { get; set; } = [];

    public List<PollResponse> Responses { get; set; } = [];

    [Required]
    public User CreatedBy { get; set; }
}
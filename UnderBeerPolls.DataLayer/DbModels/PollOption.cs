using System.ComponentModel.DataAnnotations;
using UnderBeerPolls.DataLayer.Enums;

namespace UnderBeerPolls.DataLayer.DbModels;

public class PollOption
{
    public long Id { get; set; }
    
    [Required]
    [MaxLength(3000)]
    public string Question { get; set; }

    [Required]
    public PollType Type { get; set; }

    public List<string>? Options  { get; set; } = [];

    public Guid PollId { get; set; }
}
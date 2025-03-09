using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UnderBeerPolls.DataLayer.DbModels;

public class PollOptionResponse
{
    public long Id { get; set; }
    
    [JsonIgnore]
    public PollOption PollOption { get; set; }
    
    public long PollOptionId { get; set; }
    
    [Required]
    public string Response { get; set; }
}
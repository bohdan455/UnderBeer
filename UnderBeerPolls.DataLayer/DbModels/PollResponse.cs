using System.Text.Json.Serialization;

namespace UnderBeerPolls.DataLayer.DbModels;

public class PollResponse
{
    public long Id { get; set; }

    [JsonIgnore] 
    public Guid PollId { get; set; }
    
    [JsonIgnore]
    public Poll Poll { get; set; }

    public List<PollOptionResponse> PollOptionResponses { get; set; } = [];
}
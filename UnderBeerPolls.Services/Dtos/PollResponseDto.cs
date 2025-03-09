namespace UnderBeerPolls.Services.Dtos;

public class PollResponseDto
{
    public Guid PollId { get; set; }

    public Dictionary<long, string> Answers { get; set; }
}
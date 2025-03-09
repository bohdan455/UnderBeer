namespace UnderBeerPolls.Services.Dtos;

public class PollShortInformationDto
{
    public Guid PollId { get; set; }

    public string Title { get; set; }

    public int NumberOfResponses { get; set; }
}
using UnderBeerPolls.Services.Dtos;

namespace UnderBeerPolls.Api.Models;

public class PollResponseModel
{
    public Guid PollId { get; set; }

    public Dictionary<long, string> Answers { get; set; }

    public PollResponseDto ToResponseDto()
    {
        return new PollResponseDto
        {
            Answers = Answers,
            PollId = PollId,
        };
    }
}
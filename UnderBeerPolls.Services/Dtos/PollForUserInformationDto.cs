using UnderBeerPolls.DataLayer.DbModels;

namespace UnderBeerPolls.Services.Dtos;

public class PollForUserInformationDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public List<PollOption> Options { get; set; } = [];
}
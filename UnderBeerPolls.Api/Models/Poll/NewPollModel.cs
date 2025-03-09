using UnderBeerPolls.DataLayer.DbModels;
using UnderBeerPolls.Services.Dtos;

namespace UnderBeerPolls.Api.Controllers;

public class NewPollModel
{
    public string Title { get; set; }

    public List<PollOptionModel> Options { get; set; }

    public Poll ToPollModel()
    {
        return new Poll
        {
            Id = Guid.NewGuid(),
            Title = Title,
            Options = Options.Select(x => new PollOption
            {
                Options = x.Options,
                Question = x.Question,
                Type = x.Type
            }).ToList()
        };
    }
}
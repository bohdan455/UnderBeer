using UnderBeerPolls.DataLayer.Enums;

namespace UnderBeerPolls.Api.Controllers;

public class PollOptionModel
{
    public string Question { get; set; }

    public PollType Type { get; set; }

    public List<string> Options  { get; set; }
}
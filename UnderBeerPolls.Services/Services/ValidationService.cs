using UnderBeerPolls.DataLayer.DbModels;
using UnderBeerPolls.DataLayer.Enums;
using UnderBeerPolls.Services.Dtos;
using UnderBeerPolls.Services.Exceptions;
using UnderBeerPolls.Services.Services.Interfaces;

namespace UnderBeerPolls.Services.Services;

public class ValidationService : IValidationService
{
    public void ValidatePollSubmit(PollResponseDto responseDto, IReadOnlyCollection<PollOption> pollOptions)
    {
        if (!responseDto.Answers.All(a => pollOptions.Any(p => p.Id == a.Key)))
        {
            throw new InvalidResponseOptionsException();
        }

        var invalidValues = new List<long>();

        foreach (var answer in responseDto.Answers)
        {
            if (!ValidateOptionType(answer.Value, pollOptions.First(x => x.Id == answer.Key)))
            {
                invalidValues.Add(answer.Key);
            }
        }

        if (invalidValues.Count != 0)
        {
            throw new InvalidOptionResponseValueException(invalidValues);
        }
    }

    private bool ValidateOptionType(string value, PollOption pollOption)
    {
        return pollOption.Type switch
        {
            PollType.Number => double.TryParse(value, out _),
            PollType.Text => !string.IsNullOrEmpty(value),
            PollType.MultipleChoice => pollOption.Options != null && pollOption.Options.Contains(value),
            _ => throw new NotImplementedException()
        };
    }
}
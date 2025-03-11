using UnderBeerPolls.DataLayer.DbModels;
using UnderBeerPolls.Services.Dtos;

namespace UnderBeerPolls.Services.Services.Interfaces;

public interface IValidationService
{
    void ValidatePollSubmit(PollResponseDto responseDto, IReadOnlyCollection<PollOption> pollOptions);
}
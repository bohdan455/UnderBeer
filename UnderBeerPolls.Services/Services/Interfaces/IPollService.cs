using UnderBeerPolls.DataLayer.DbModels;
using UnderBeerPolls.Services.Dtos;

namespace UnderBeerPolls.Services.Services.Interfaces;

public interface IPollService
{
    Task CreatePoll(string username, Poll newPoll);

    Task<Poll> GetPollFullInformationForCreator(string username, Guid pollId);
    
    Task<PollForUserInformationDto> GetPollFullInformationForUser(Guid pollId);
    
    Task<List<PollShortInformationDto>> GetAllPollsForUser(string username);

    Task<bool> SubmitPoll(PollResponseDto pollResponseDto);
}
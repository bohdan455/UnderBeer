using Microsoft.EntityFrameworkCore;
using UnderBeerPolls.DataLayer;
using UnderBeerPolls.DataLayer.DbModels;
using UnderBeerPolls.Services.Dtos;
using UnderBeerPolls.Services.Exceptions;
using UnderBeerPolls.Services.Services.Interfaces;

namespace UnderBeerPolls.Services.Services;

public class PollService : IPollService
{
    private readonly ApplicationDbContext _context;
    private readonly IValidationService _validationService;

    public PollService(ApplicationDbContext context, IValidationService validationService)
    {
        _context = context;
        _validationService = validationService;
    }

    public async Task CreatePoll(string username, Poll newPoll)
    {
        var user = await _context.Users.FirstAsync(x => x.Username == username);
        newPoll.CreatedBy = user;
        _context.Polls.Add(newPoll);
        await _context.SaveChangesAsync();
    }

    public async Task<Poll> GetPollFullInformationForCreator(string username, Guid pollId)
    {
        var poll = await _context.Polls
            .Include(x => x.Options)
            .Include(x => x.Responses).ThenInclude(x => x.PollOptionResponses).ThenInclude(x => x.PollOption)
            .Include(x => x.CreatedBy)
            .FirstOrDefaultAsync(x => x.Id == pollId && x.CreatedBy.Username == username);

        if (poll == null)
        {
            throw new InvalidPollException(pollId);
        }
        
        return poll;
    }

    public async Task<PollForUserInformationDto> GetPollFullInformationForUser(Guid pollId)
    {
        var poll = await _context.Polls
            .Include(x => x.Options)
            .Select(x => new PollForUserInformationDto
            {
                Id = pollId,
                Options = x.Options,
                Title = x.Title
            })
            .FirstOrDefaultAsync(x => x.Id == pollId);

        if (poll == null)
        {
            throw new InvalidPollException(pollId);
        }
        
        return poll;
    }

    public async Task<List<PollShortInformationDto>> GetAllPollsForUser(string username)
    {
        var polls = await _context.Polls
            .Include(x => x.Responses)
            .Include(x => x.CreatedBy)
            .Where(x => x.CreatedBy.Username == username)
            .Select(x => new PollShortInformationDto
            {
                PollId = x.Id,
                Title = x.Title,
                NumberOfResponses = x.Responses.Count()
            })
            .ToListAsync();

        return polls;
    }
    
    public async Task<bool> SubmitPoll(PollResponseDto pollResponseDto)
    {
        var options = await _context.PollOptions
            .Where(x => x.PollId == pollResponseDto.PollId)
            .ToListAsync();

        if (options.Count == 0)
        {
            throw new InvalidPollException(pollResponseDto.PollId);
        }
        
        _validationService.ValidatePollSubmit(pollResponseDto, options);
        
        _context.PollResponses.Add(new PollResponse
        {
            PollId = pollResponseDto.PollId,
            PollOptionResponses = pollResponseDto.Answers.Select(x => new PollOptionResponse
            {
                PollOptionId = x.Key,
                Response = x.Value
            }).ToList()
        });

        await _context.SaveChangesAsync();

        return true;
    }
}
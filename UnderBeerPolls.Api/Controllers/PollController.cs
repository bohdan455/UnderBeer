using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnderBeerPolls.Api.Extensions;
using UnderBeerPolls.Api.Models;
using UnderBeerPolls.Services.Dtos;
using UnderBeerPolls.Services.Services.Interfaces;

namespace UnderBeerPolls.Api.Controllers;

[ApiController]
[Route("api/polls")]
public class PollController : ControllerBase
{
    private readonly IPollService _pollService;
    private readonly ILogger<PollController> _logger;

    public PollController(IPollService pollService, ILogger<PollController> logger)
    {
        _pollService = pollService;
        _logger = logger;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllPolls()
    {
        var polls = await _pollService.GetAllPollsForUser(User.GetUsername());

        return Ok(polls);
    }

    [HttpGet("user/{id:guid}")]
    public async Task<IActionResult> GetPollFullInformationForUser([FromRoute] Guid id)
    {
        var poll = await _pollService.GetPollFullInformationForUser(id);

        return Ok(poll);
    }
    
    [Authorize]
    [HttpGet("creator/{id:guid}")]
    public async Task<IActionResult> GetPollFullInformationForCreator([FromRoute] Guid id)
    {
        var poll = await _pollService.GetPollFullInformationForCreator(User.GetUsername(), id);

        return Ok(poll);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateNewPoll([FromBody] NewPollModel pollModel)
    {
        await _pollService.CreatePoll(User.GetUsername(), pollModel.ToPollModel());
        _logger.LogInformation("New poll created {@poll}", pollModel);
        return Ok();
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitPoll([FromBody] PollResponseModel pollResponseModel)
    {
        await _pollService.SubmitPoll(pollResponseModel.ToResponseDto());
        _logger.LogInformation("New poll submit {@pollSubmit}", pollResponseModel);
        return Ok();
    }
}
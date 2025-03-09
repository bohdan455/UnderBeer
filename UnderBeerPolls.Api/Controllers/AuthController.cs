using Microsoft.AspNetCore.Mvc;
using UnderBeerPolls.Api.Models;
using UnderBeerPolls.Services.Services.Interfaces;

namespace UnderBeerPolls.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthModel authModel)
    {
        var result = await _authService.Login(authModel.Username, authModel.Password);
        return string.IsNullOrEmpty(result) ? BadRequest() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Register(AuthModel authModel)
    {
        var result = await _authService.Register(authModel.Username, authModel.Password);

        return result ? Ok() : BadRequest();
    }
}
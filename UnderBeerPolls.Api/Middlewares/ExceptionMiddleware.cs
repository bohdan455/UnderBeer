using System.Net;
using System.Text.Json;
using UnderBeerPolls.Api.Models.Base;
using UnderBeerPolls.Services.Exceptions;

namespace UnderBeerPolls.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            var (status, message) = GetResponse(ex);
            var response = context.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            await response.WriteAsync(message);

        }
    }
    
    public (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        HttpStatusCode code;
        switch (exception)
        {
            case FailedLoginAttemptException
                or UserAlreadyExistsException
                or InvalidResponseOptionsException
                or InvalidPollException:
                code = HttpStatusCode.BadRequest;
                break;
            case InvalidOptionResponseValueException ex:
                code = HttpStatusCode.BadRequest;
                return (code, JsonSerializer.Serialize(new ErrorModel<List<long>>(exception.Message, ex.FailedOptions)));
            default:
                _logger.LogError(exception.ToString());
                code = HttpStatusCode.InternalServerError;
                break;
        }
        return (code, JsonSerializer.Serialize(new ErrorModel(exception.Message)));
    }
}
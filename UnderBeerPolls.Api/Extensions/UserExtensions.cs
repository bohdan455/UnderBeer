using System.Security.Claims;

namespace UnderBeerPolls.Api.Extensions;

public static class UserExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.Claims.First(x => x.Type == "name").Value;
    }
}
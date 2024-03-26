using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Service;

public static class AuthHelper
{
    public static Task<Guid> GetUserId(ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User not authenticated");
        }
            
        var userId = Guid.Parse(userIdClaim.Value);
        return Task.FromResult(userId);
    }
}
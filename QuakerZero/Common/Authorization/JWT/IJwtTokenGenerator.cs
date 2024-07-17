using System.Net;
using System.Security.Claims;

namespace QuakerZero
{
    /// <summary>
    /// Interface for Jwt Token generating classes
    /// </summary>
    public interface IJwtTokenGenerator{
        string GenerateToken(string number, string contractId, Claim[] claims, int expirationInMinutes);
        ClaimsPrincipal ValidateJwtToken(string token);
        string GetClaim(string type, ClaimsPrincipal principal);
        string GetBearerToken(HttpListenerContext context);
        string GetBearerToken(string socketHeader);
    }
}

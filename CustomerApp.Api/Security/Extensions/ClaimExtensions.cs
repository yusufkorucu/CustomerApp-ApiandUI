using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace CustomerApp.Api.Security.Extensions;

public static class ClaimExtensions
{
    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new Claim(type: JwtRegisteredClaimNames.Email, value: email));
    }
    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        claims.Add(new Claim(type: ClaimTypes.NameIdentifier, value: nameIdentifier));
    }
}
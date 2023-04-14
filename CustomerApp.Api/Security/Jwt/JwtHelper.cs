using CustomerApp.Api.Model.User;
using CustomerApp.Api.Security.Encyption;
using CustomerApp.Api.Security.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CustomerApp.Api.Security.Jwt;

public class JwtHelper : ITokenHelper
{
    #region Field

    public IConfiguration Configuration { get; }
    private TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    #endregion

    #region  Ctor

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();
    }

    #endregion

    #region Methods

    public AccessToken CreateToken(User user)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };

    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials)
    {
        var Jwt = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClains(user),
            signingCredentials: signingCredentials
        );
        return Jwt;
    }

    #endregion

    #region Util
    private IEnumerable<Claim> SetClains(User user)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        return claims;
    }
    #endregion
}
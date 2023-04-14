using CustomerApp.Api.Model.User;

namespace CustomerApp.Api.Security.Jwt;

public interface ITokenHelper
{
    AccessToken CreateToken(User model);
}
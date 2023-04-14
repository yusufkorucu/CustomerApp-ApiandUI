using CustomerApp.Api.Model.Base;

namespace CustomerApp.Api.Model.User;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
}
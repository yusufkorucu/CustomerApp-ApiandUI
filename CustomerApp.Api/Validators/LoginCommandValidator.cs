using CustomerApp.Api.Handlers.Command;
using FluentValidation;

namespace CustomerApp.Api.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().NotNull();
    }
}
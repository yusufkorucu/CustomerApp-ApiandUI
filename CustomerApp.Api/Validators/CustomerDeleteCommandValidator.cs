using CustomerApp.Api.Handlers.Command;
using FluentValidation;

namespace CustomerApp.Api.Validators;

public class CustomerDeleteCommandValidator : AbstractValidator<CustomerDeleteCommand>
{
    public CustomerDeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
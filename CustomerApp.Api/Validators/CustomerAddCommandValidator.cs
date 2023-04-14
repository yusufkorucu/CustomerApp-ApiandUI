using CustomerApp.Api.Handlers.Command;
using FluentValidation;

namespace CustomerApp.Api.Validators;

public class CustomerAddCommandValidator : AbstractValidator<CustomerAddCommand>
{
    public CustomerAddCommandValidator()
    {
        RuleFor(x => x.Tckno).NotEmpty().NotNull();
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.LastName).NotEmpty().NotNull();
        RuleFor(x => x.BirthDate).NotEmpty().NotNull();
        RuleFor(x => x.Phone).NotEmpty().NotNull();
    }
}
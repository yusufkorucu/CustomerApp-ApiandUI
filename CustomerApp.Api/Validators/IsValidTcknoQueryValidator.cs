using CustomerApp.Api.Handlers.Queries;
using FluentValidation;

namespace CustomerApp.Api.Validators;

public class IsValidTcknoQueryValidator : AbstractValidator<IsValidTcknoQuery>
{
    public IsValidTcknoQueryValidator()
    {
        RuleFor(x => x.Tckno).NotEmpty().NotNull();
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.LastName).NotEmpty().NotNull();
        RuleFor(x => x.BirthDate).NotEmpty().NotNull();
    }
}
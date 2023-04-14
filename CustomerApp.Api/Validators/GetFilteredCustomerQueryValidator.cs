using CustomerApp.Api.Handlers.Queries;
using FluentValidation;

namespace CustomerApp.Api.Validators;

public class GetFilteredCustomerQueryValidator : AbstractValidator<GetFilteredCustomerQuery>
{
    public GetFilteredCustomerQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.LastName).NotEmpty().NotNull();
        RuleFor(x => x.Tckno).NotEmpty().NotNull();
    }
}
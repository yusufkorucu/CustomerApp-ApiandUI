using CustomerApp.Api.Handlers.Queries;
using FluentValidation;

namespace CustomerApp.Api.Validators;

public class GetCustomerByTcknoQueryValidator : AbstractValidator<GetCustomerByTcknoQuery>
{
    public GetCustomerByTcknoQueryValidator()
    {
        RuleFor(x => x.Tckno).NotEmpty().NotNull();
    }
}
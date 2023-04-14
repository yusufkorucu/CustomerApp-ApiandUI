using CustomerApp.Api.Handlers.Queries;
using FluentValidation;

namespace CustomerApp.Api.Validators;

public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
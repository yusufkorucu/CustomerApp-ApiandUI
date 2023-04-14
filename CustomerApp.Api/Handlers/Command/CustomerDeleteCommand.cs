using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Services.Abstract;
using FluentValidation;
using MediatR;

namespace CustomerApp.Api.Handlers.Command
{
    public class CustomerDeleteCommand : IRequest<CustomerApiResponse<bool>>
    {
        public int Id { get; set; }

        public class CustomerDeleteCommandHandler : IRequestHandler<CustomerDeleteCommand, CustomerApiResponse<bool>>
        {
            #region Field

            private readonly ILogger<CustomerDeleteCommandHandler> _logger;
            private ICustomerService _customerService;
            private IValidator<CustomerDeleteCommand> _validator;

            #endregion

            #region Ctor

            public CustomerDeleteCommandHandler(ILogger<CustomerDeleteCommandHandler> logger, ICustomerService customerService, IValidator<CustomerDeleteCommand> validator)
            {
                _logger = logger;
                _customerService = customerService;
                _validator = validator;
            }

            #endregion

            #region Methods

            public async Task<CustomerApiResponse<bool>> Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
            {
                var modelIsValid = await _validator.ValidateAsync(request);

                if (!modelIsValid.IsValid)
                    return new CustomerApiResponse<bool>(false,
                        modelIsValid.Errors.Select(x => x.ErrorMessage).ToList());

                try
                {
                    return await _customerService.DeleteCustomerById(request);

                }
                catch (Exception ex)
                {

                    var errorMessage = string.Format(CoreMessage.UnExpectedError, GetType().Name, ex.Message);
                    _logger.LogError(errorMessage);

                    return new CustomerApiResponse<bool>(false, ex.Message);
                }
            }

            #endregion
        }
    }
}

using CustomerApp.Api.Handlers.Queries;
using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Services.Abstract;
using CustomerApp.Api.Services.Dtos;
using FluentValidation;
using MediatR;

namespace CustomerApp.Api.Handlers.Command
{
    public class CustomerAddCommand : IRequest<CustomerApiResponse<CustomerDetailResponseDto>>
    {
        public long Tckno { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }

        public class CustomerAddCommandHandler : IRequestHandler<CustomerAddCommand, CustomerApiResponse<CustomerDetailResponseDto>>
        {
            #region Field

            private readonly ILogger<CustomerAddCommandHandler> _logger;
            private ICustomerService _customerService;
            private IMediator _mediator;
            private IValidator<CustomerAddCommand> _validator;
            #endregion

            #region Ctor

            public CustomerAddCommandHandler(ILogger<CustomerAddCommandHandler> logger, ICustomerService customerService, IMediator mediator, IValidator<CustomerAddCommand> validator)
            {
                _logger = logger;
                _customerService = customerService;
                _mediator = mediator;
                _validator = validator;
            }

            #endregion

            #region Methods

            public async Task<CustomerApiResponse<CustomerDetailResponseDto>> Handle(CustomerAddCommand request, CancellationToken cancellationToken)
            {
                var modelIsValid = await _validator.ValidateAsync(request);

                if (!modelIsValid.IsValid)
                    return new CustomerApiResponse<CustomerDetailResponseDto>(false,
                        modelIsValid.Errors.Select(x => x.ErrorMessage).ToList());

                try
                {
                    var isValidQuery = new IsValidTcknoQuery
                    {
                        BirthDate = request.BirthDate,
                        LastName = request.LastName,
                        Name = request.Name,
                        Tckno = request.Tckno
                    };

                    var result = await _mediator.Send(isValidQuery);

                    if (!result.IsSuccess)
                        return new CustomerApiResponse<CustomerDetailResponseDto>(false, string.Format(CoreMessage.NotValidTckno, request.Tckno));

                    return await _customerService.AddCustomer(request);
                }
                catch (Exception ex)
                {
                    var errorMessage = string.Format(CoreMessage.UnExpectedError, GetType().Name, ex.Message);

                    _logger.LogError(errorMessage);

                    return new CustomerApiResponse<CustomerDetailResponseDto>(false, ex.Message);
                }
            }

            #endregion
        }
    }
}

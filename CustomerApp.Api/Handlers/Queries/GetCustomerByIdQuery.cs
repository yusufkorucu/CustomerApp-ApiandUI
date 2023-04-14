using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Services.Abstract;
using CustomerApp.Api.Services.Dtos;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace CustomerApp.Api.Handlers.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerApiResponse<CustomerDetailResponseDto>>
    {
        public int Id { get; set; }

        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerApiResponse<CustomerDetailResponseDto>>
        {
            #region Field

            private ILogger<GetCustomerByIdQueryHandler> _logger;
            private ICustomerService _customerService;
            private IValidator<GetCustomerByIdQuery> _validator;

            #endregion

            #region Ctor

            public GetCustomerByIdQueryHandler(ILogger<GetCustomerByIdQueryHandler> logger, ICustomerService customerService, IValidator<GetCustomerByIdQuery> validator)
            {
                _logger = logger;
                _customerService = customerService;
                _validator = validator;
            }

            #endregion

            #region Methods

            public async Task<CustomerApiResponse<CustomerDetailResponseDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {

                var modelIsValid = await _validator.ValidateAsync(request);

                if (!modelIsValid.IsValid)
                    return new CustomerApiResponse<CustomerDetailResponseDto>(false,
                        modelIsValid.Errors.Select(x => x.ErrorMessage).ToList());
                try
                {
                    var response = await _customerService.GetCustomerDetailById(request);

                    _logger.LogInformation($" {GetType().Name}  Request : {request.Id} Csutomer Get By Respone : {JsonConvert.SerializeObject(response.Data)}");

                    return response;

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

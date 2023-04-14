using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Services.Abstract;
using CustomerApp.Api.Services.Dtos;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace CustomerApp.Api.Handlers.Queries
{
    public class GetCustomerByTcknoQuery : IRequest<CustomerApiResponse<CustomerDetailResponseDto>>
    {
        public long Tckno { get; set; }

        public class GetCustomerByTcknoQueryHandler : IRequestHandler<GetCustomerByTcknoQuery, CustomerApiResponse<CustomerDetailResponseDto>>
        {
            #region Field

            private ILogger<GetCustomerByTcknoQueryHandler> _logger;
            private ICustomerService _customerService;
            private IValidator<GetCustomerByTcknoQuery> _validator;


            #endregion

            #region Ctor

            public GetCustomerByTcknoQueryHandler(ILogger<GetCustomerByTcknoQueryHandler> logger, ICustomerService customerService, IValidator<GetCustomerByTcknoQuery> validator)
            {
                _logger = logger;
                _customerService = customerService;
                _validator = validator;
            }

            #endregion

            #region Methods

            public async Task<CustomerApiResponse<CustomerDetailResponseDto>> Handle(GetCustomerByTcknoQuery request, CancellationToken cancellationToken)
            {
                var modelIsValid = await _validator.ValidateAsync(request);

                if (!modelIsValid.IsValid)
                    return new CustomerApiResponse<CustomerDetailResponseDto>(false,
                        modelIsValid.Errors.Select(x => x.ErrorMessage).ToList());
                try
                {
                    var response = await _customerService.GetCustomerDetailByTckno(request);

                    _logger.LogInformation($"{GetType().Name} Request {request.Tckno} Csutomer Get By Tckno response : {JsonConvert.SerializeObject(response.Data)}");

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

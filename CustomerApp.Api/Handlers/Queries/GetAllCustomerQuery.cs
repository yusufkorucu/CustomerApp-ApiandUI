using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Services.Abstract;
using CustomerApp.Api.Services.Dtos;
using MediatR;
using Newtonsoft.Json;

namespace CustomerApp.Api.Handlers.Queries
{
    public class GetAllCustomerQuery : IRequest<CustomerApiResponse<List<CustomerDetailResponseDto>>>
    {

        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, CustomerApiResponse<List<CustomerDetailResponseDto>>>
        {
            #region Field

            private ILogger<GetAllCustomerQueryHandler> _logger;
            private ICustomerService _customerService;

            #endregion

            #region Ctor

            public GetAllCustomerQueryHandler(ILogger<GetAllCustomerQueryHandler> logger, ICustomerService customerService)
            {
                _logger = logger;
                _customerService = customerService;
            }

            #endregion

            #region Methods

            public async Task<CustomerApiResponse<List<CustomerDetailResponseDto>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var response = await _customerService.GetAllCustomerDetails();

                    _logger.LogInformation($" {GetType().Name}  Request :  Csutomer Get By Respone : {JsonConvert.SerializeObject(response.Data)}");

                    return response;

                }
                catch (Exception ex)
                {

                    var errorMessage = string.Format(CoreMessage.UnExpectedError, GetType().Name, ex.Message);
                    _logger.LogError(errorMessage);

                    return new CustomerApiResponse<List<CustomerDetailResponseDto>>(false, ex.Message);
                }
            }

            #endregion
        }
    }
}

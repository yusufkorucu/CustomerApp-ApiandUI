using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Services.Abstract;
using CustomerApp.Api.Services.Dtos;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace CustomerApp.Api.Handlers.Queries
{
    public class GetFilteredCustomerQuery : IRequest<CustomerApiResponse<List<CustomerDetailResponseDto>>>
    {
        public long Tckno { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public class GetFilteredCustomerQueryHandler : IRequestHandler<GetFilteredCustomerQuery, CustomerApiResponse<List<CustomerDetailResponseDto>>>
        {
            #region Field

            private ILogger<GetFilteredCustomerQueryHandler> _logger;
            private ICustomerService _customerService;
            private IValidator<GetFilteredCustomerQuery> _validator;


            #endregion

            #region Ctor

            public GetFilteredCustomerQueryHandler(ILogger<GetFilteredCustomerQueryHandler> logger, ICustomerService customerService, IValidator<GetFilteredCustomerQuery> validator)
            {
                _logger = logger;
                _customerService = customerService;
                _validator = validator;
            }

            #endregion

            #region Methods

            public async Task<CustomerApiResponse<List<CustomerDetailResponseDto>>> Handle(GetFilteredCustomerQuery request, CancellationToken cancellationToken)
            {
                var modelIsValid = await _validator.ValidateAsync(request);

                //if (!modelIsValid.IsValid)
                //    return new CustomerApiResponse<List<CustomerDetailResponseDto>>(false,
                //        modelIsValid.Errors.Select(x => x.ErrorMessage).ToList());
                try
                {
                    var response = await _customerService.GetFilteredCustomerDetails(request);

                    _logger.LogInformation($"{GetType().Name} Request:{JsonConvert.SerializeObject(request)} -Response : {JsonConvert.SerializeObject(response.Data)}");

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

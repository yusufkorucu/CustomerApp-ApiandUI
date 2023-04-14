using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Services.Abstract;
using CustomerApp.Api.Services.Dtos;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace CustomerApp.Api.Handlers.Queries
{
    public class IsValidTcknoQuery : IRequest<CustomerApiResponse<KPSServiseResponseDto>>
    {
        public long Tckno { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public class IsValidTcknoQueryHandler : IRequestHandler<IsValidTcknoQuery, CustomerApiResponse<KPSServiseResponseDto>>
        {
            #region Field

            private ILogger<IsValidTcknoQueryHandler> _logger;
            private ICustomerService _customerService;
            private IValidator<IsValidTcknoQuery> _validator;

            #endregion

            #region Ctor

            public IsValidTcknoQueryHandler(ILogger<IsValidTcknoQueryHandler> logger, ICustomerService customerService, IValidator<IsValidTcknoQuery> validator)
            {
                _logger = logger;
                _customerService = customerService;
                _validator = validator;
            }

            #endregion

            #region Methods

            public async Task<CustomerApiResponse<KPSServiseResponseDto>> Handle(IsValidTcknoQuery request, CancellationToken cancellationToken)
            {
                var modelIsValid = await _validator.ValidateAsync(request);

                if (!modelIsValid.IsValid)
                    return new CustomerApiResponse<KPSServiseResponseDto>(false,
                        modelIsValid.Errors.Select(x => x.ErrorMessage).ToList());

                try
                {
                    var response = await _customerService.IsValidTckno(request);

                    _logger.LogInformation($"{GetType().Name} Request:{JsonConvert.SerializeObject(request)} -KPS Service Response : {JsonConvert.SerializeObject(response.Data)}");

                    return response;

                }
                catch (Exception ex)
                {

                    var errorMessage = string.Format(CoreMessage.UnExpectedError, GetType().Name, ex.Message);
                    _logger.LogError(errorMessage);

                    return new CustomerApiResponse<KPSServiseResponseDto>(false, ex.Message);
                }
            }

            #endregion
        }
    }
}

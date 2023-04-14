using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Model.User;
using CustomerApp.Api.Security.Jwt;
using FluentValidation;
using MediatR;

namespace CustomerApp.Api.Handlers.Command;

public class LoginCommand : IRequest<CustomerApiResponse<AccessToken>>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, CustomerApiResponse<AccessToken>>
    {
        #region Field
        private readonly ITokenHelper _tokenHelper;
        private readonly User _user;
        private readonly IValidator<LoginCommand> _validator;
        private readonly ILogger<LoginCommandHandler> _logger;
        #endregion

        #region Ctor
        public LoginCommandHandler(ITokenHelper tokenHelper, IValidator<LoginCommand> validator, ILogger<LoginCommandHandler> logger)
        {
            _tokenHelper = tokenHelper;
            _validator = validator;
            _logger = logger;
            _user = new User()
            {
                Id = 1,
                Email = "yusuf@admin.com",
                Password = "admin"
            };
        }
        #endregion

        #region Methods
        public async Task<CustomerApiResponse<AccessToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var modelIsValid = await _validator.ValidateAsync(request);

            if (!modelIsValid.IsValid)
                return new CustomerApiResponse<AccessToken>(false,
                    modelIsValid.Errors.Select(x => x.ErrorMessage).ToList());

            try
            {
                if (request.Email != _user.Email || request.Password != _user.Password)
                    return new CustomerApiResponse<AccessToken>(false, CoreMessage.AuthenticateError);

                var token = _tokenHelper.CreateToken(_user);

                return new CustomerApiResponse<AccessToken>(true, token);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(CoreMessage.UnExpectedError, GetType().Name, ex.Message);
                _logger.LogError(errorMessage);

                return new CustomerApiResponse<AccessToken>(false, ex.Message);
            }
        }
        #endregion
    }
}
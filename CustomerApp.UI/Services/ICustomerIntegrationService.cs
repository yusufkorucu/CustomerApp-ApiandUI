using CustomerApp.Ui.Models;
using CustomerApp.UI.Models;

namespace CustomerApp.Ui.Services;

public interface ICustomerIntegrationService
{
    Task<ResponseDto> Login(LoginViewModel model);
    Task<ResponseDto> CreateCustomer(CustomerAddViewModel model, string token);
    Task<ResponseDto> DeleteCustomer(int id, string token);

    Task<CustomerDetailResponseDto<List<CustomerDetailsDto>>> GetFilteredCustomer(CustomerGetFilteredViewModel model, string token);
    Task<CustomerDetailResponseDto<CustomerDetailsDto>> GetDetailCustomer(int id, string token);
    Task<CustomerDetailResponseDto<List<CustomerDetailsDto>>> GetAllCustomer(string token);

}
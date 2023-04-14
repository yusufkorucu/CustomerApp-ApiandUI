using CustomerApp.Api.Handlers.Command;
using CustomerApp.Api.Handlers.Queries;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Services.Dtos;

namespace CustomerApp.Api.Services.Abstract
{
    public interface ICustomerService
    {
        Task<CustomerApiResponse<CustomerDetailResponseDto>> AddCustomer(CustomerAddCommand command);
        Task<CustomerApiResponse<CustomerDetailResponseDto>> GetCustomerDetailById(GetCustomerByIdQuery query);
        Task<CustomerApiResponse<CustomerDetailResponseDto>> GetCustomerDetailByTckno(GetCustomerByTcknoQuery query);
        Task<CustomerApiResponse<List<CustomerDetailResponseDto>>> GetFilteredCustomerDetails(GetFilteredCustomerQuery query);
        Task<CustomerApiResponse<List<CustomerDetailResponseDto>>> GetAllCustomerDetails();
        Task<CustomerApiResponse<bool>> DeleteCustomerById(CustomerDeleteCommand command);
        Task<CustomerApiResponse<KPSServiseResponseDto>> IsValidTckno(IsValidTcknoQuery query);
        Task<CustomerApiResponse<bool>> IsExistCustomer(long tckno);
    }
}

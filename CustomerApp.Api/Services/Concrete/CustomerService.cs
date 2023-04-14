using AutoMapper;
using CustomerApp.Api.Handlers.Command;
using CustomerApp.Api.Handlers.Queries;
using CustomerApp.Api.Infrastructure.Constants;
using CustomerApp.Api.Infrastructure.Extentions;
using CustomerApp.Api.Infrastructure.Utilities;
using CustomerApp.Api.Model;
using CustomerApp.Api.Repositories.Customer;
using CustomerApp.Api.Services.Abstract;
using CustomerApp.Api.Services.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CustomerApp.Api.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        #region Field
        private readonly ICustomerRepository _customerRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CustomerService(ICustomerRepository customerRepository, IMemoryCache memoryCache, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        #endregion

        #region Methods
        public async Task<CustomerApiResponse<CustomerDetailResponseDto>> AddCustomer(CustomerAddCommand command)
        {
            var customer = PrepareAddCustomerData(command);

            var isExist = await IsExistCustomer(customer.Tckno);

            if (isExist.IsSuccess)
                return new CustomerApiResponse<CustomerDetailResponseDto>(false, CoreMessage.ExistCustomer);

            await _customerRepository.AddAsync(customer);

            var result = await _customerRepository.CompleteAsync();

            if (result == 1)

                return new CustomerApiResponse<CustomerDetailResponseDto>(true, CoreMessage.AddedCustomerSuccessfuly);

            else

                return new CustomerApiResponse<CustomerDetailResponseDto>(false, CoreMessage.FailAddedCustomer);
        }

        public async Task<CustomerApiResponse<bool>> DeleteCustomerById(CustomerDeleteCommand command)
        {

            var result = await _customerRepository.DeleteCustomerAsync(command.Id);

            return new CustomerApiResponse<bool>(result);

        }

        public async Task<CustomerApiResponse<CustomerDetailResponseDto>> GetCustomerDetailById(GetCustomerByIdQuery query)
        {
            if (_memoryCache.TryGetValue(CacheKeys.GetCustomerDetailId + query.Id.ToString(), out CustomerDetailResponseDto cacheResponse))
                return new CustomerApiResponse<CustomerDetailResponseDto>(true, cacheResponse);


            var result = await _customerRepository.GetByIdAsync(query.Id);

            if (result == null)
                return new CustomerApiResponse<CustomerDetailResponseDto>(false, CoreMessage.NotFoundCustomer);

            var response = _mapper.Map<CustomerDetailResponseDto>(result);

            response = MaskSensiteveDataExtentions.MaskedData(response);

            _memoryCache.Set(CacheKeys.GetCustomerDetailId + query.Id.ToString(), response, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                Priority = CacheItemPriority.Normal
            });

            return new CustomerApiResponse<CustomerDetailResponseDto>(true, response);

        }

        public async Task<CustomerApiResponse<CustomerDetailResponseDto>> GetCustomerDetailByTckno(GetCustomerByTcknoQuery query)
        {
            if (_memoryCache.TryGetValue(CacheKeys.GetCustomerDetailTckno + query.Tckno.ToString(), out CustomerDetailResponseDto cacheResponse))
                return new CustomerApiResponse<CustomerDetailResponseDto>(true, cacheResponse);

            var result = await _customerRepository.GetByTckno(query.Tckno);

            if (result == null)
                return new CustomerApiResponse<CustomerDetailResponseDto>(false, CoreMessage.NotFoundCustomer);

            var response = _mapper.Map<CustomerDetailResponseDto>(result);

            response = MaskSensiteveDataExtentions.MaskedData(response);


            _memoryCache.Set(CacheKeys.GetCustomerDetailTckno + query.Tckno.ToString(), response, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                Priority = CacheItemPriority.Normal
            });

            return new CustomerApiResponse<CustomerDetailResponseDto>(true, response);
        }

        public async Task<CustomerApiResponse<List<CustomerDetailResponseDto>>> GetFilteredCustomerDetails(GetFilteredCustomerQuery query)
        {
            var querys = _customerRepository.GetAll().AsQueryable();

            try
            {
                if (query.Tckno > 1)
                    querys = querys.Where(x => x.Tckno == query.Tckno);

                if (!string.IsNullOrEmpty(query.Name))
                    querys = querys.Where(x => x.Name == query.Name);

                if (!string.IsNullOrEmpty(query.LastName))
                    querys = querys.Where(x => x.LastName == query.LastName);

                var customers = await querys.Where(x => x.IsDelete == false).ToListAsync();

                var response = _mapper.Map<List<CustomerDetailResponseDto>>(customers);

                response = MaskSensiteveDataExtentions.MaskedListData(response);

                return new CustomerApiResponse<List<CustomerDetailResponseDto>>(true, response);
            }
            catch (Exception ex)
            {
                // log
                return new CustomerApiResponse<List<CustomerDetailResponseDto>>(false, ex.Message);
            }


        }

        public async Task<CustomerApiResponse<List<CustomerDetailResponseDto>>> GetAllCustomerDetails()
        {
            var querys = await _customerRepository.GetAll().AsQueryable().Where(x => x.IsDelete == false).ToListAsync();

            try
            {

                var response = _mapper.Map<List<CustomerDetailResponseDto>>(querys);

                response = MaskSensiteveDataExtentions.MaskedListData(response);


                return new CustomerApiResponse<List<CustomerDetailResponseDto>>(true, response);
            }
            catch (Exception ex)
            {
                // log
                return new CustomerApiResponse<List<CustomerDetailResponseDto>>(false, ex.Message);
            }


        }


        public async Task<CustomerApiResponse<bool>> IsExistCustomer(long tckno)
        {
            var result = await _customerRepository.IsExistCustomer(tckno);

            return new CustomerApiResponse<bool>(result);
        }

        public async Task<CustomerApiResponse<KPSServiseResponseDto>> IsValidTckno(IsValidTcknoQuery query)
        {
            using (TcknoService.KPSPublicSoapClient tcknoService = new TcknoService.KPSPublicSoapClient(TcknoService.KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap12))
            {
                var response = await tcknoService.TCKimlikNoDogrulaAsync(query.Tckno, query.Name.ToUpper(), query.LastName.ToUpper(), query.BirthDate.Year);

                if (response.Body.TCKimlikNoDogrulaResult)
                    return new CustomerApiResponse<KPSServiseResponseDto>(true, CoreMessage.RequestSuccessCompleted);

                return new CustomerApiResponse<KPSServiseResponseDto>(false, CoreMessage.NotValidTckno);

            }

        }

        private Customer PrepareAddCustomerData(CustomerAddCommand command)
        {
            return _mapper.Map<CustomerAddCommand, Customer>(command);
        }

        #endregion
    }
}

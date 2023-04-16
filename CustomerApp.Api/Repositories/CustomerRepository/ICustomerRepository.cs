using CustomerApp.Api.Repositories.GenericRepository;

namespace CustomerApp.Api.Repositories.Customer
{
    public interface ICustomerRepository : IGenericRepository<Model.Customer>
    {
        Task<Model.Customer> GetByTckno(long tckno);
        Task<bool> IsExistCustomer(long Tckno);

    }
}

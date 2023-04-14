namespace CustomerApp.Api.Repositories.Customer
{
    public interface ICustomerRepository
    {
        public Task<List<Model.Customer>> GetListAsync();
        public Task<Model.Customer> GetByIdAsync(int id);
        public Task<Model.Customer> GetByTckno(long tckno);
        public Task<Model.Customer> AddAsync(Model.Customer customer);
        public Task<bool> IsExistCustomer(long Tckno);
        public Task<int> UpdateCustomerAsync(Model.Customer customer);
        public Task<bool> DeleteCustomerAsync(int id);
        public Task<int> CompleteAsync();

        public IEnumerable<Model.Customer> GetAll();

    }
}

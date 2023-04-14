using CustomerApp.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Api.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly CustomerAppDbContext _dbContext;

        public CustomerRepository(CustomerAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Model.Customer> AddAsync(Model.Customer customer)
        {
            var result = await _dbContext.Customers.AddAsync(customer);
            return result.Entity;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();

        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            bool response = false;
            try
            {
                var customer = await GetByIdAsync(id);
                if (customer != null)
                {
                    customer.IsDelete = true;
                    var result = await UpdateCustomerAsync(customer);
                    if (result == 1)
                    {
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {

                //log
            }

            return response;

        }

        public IEnumerable<Model.Customer> GetAll()
        {
            return _dbContext.Customers.AsEnumerable();

        }

        public async Task<Model.Customer> GetByIdAsync(int id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id && x.IsDelete == false);
        }

        public async Task<Model.Customer> GetByTckno(long tckno)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Tckno == tckno && x.IsDelete == false);

        }

        public Task<List<Model.Customer>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExistCustomer(long tckno)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Tckno == tckno);
            if (customer != null)
                return true;
            return false;
        }

        public async Task<int> UpdateCustomerAsync(Model.Customer customer)
        {
            _dbContext.Customers.Update(customer);
            return await _dbContext.SaveChangesAsync();


        }
    }
}

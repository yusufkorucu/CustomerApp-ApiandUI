using CustomerApp.Api.Data;
using CustomerApp.Api.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Api.Repositories.Customer
{

    public class CustomerRepository : GenericRepository<Model.Customer>, ICustomerRepository
    {
        #region Field
        private readonly CustomerAppDbContext _dbContext;
        #endregion

        #region Ctor
        public CustomerRepository(CustomerAppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Special Methods
        public async Task<Model.Customer> GetByTckno(long tckno)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Tckno == tckno && x.IsDelete == false);

        }

        public async Task<bool> IsExistCustomer(long tckno)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Tckno == tckno);
            if (customer != null)
                return true;
            return false;
        }
        #endregion
    }
}

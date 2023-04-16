using CustomerApp.Api.Data;
using CustomerApp.Api.Model;
using CustomerApp.Api.Model.Base;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Api.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region Field
        private readonly CustomerAppDbContext _dbContext;
        protected DbSet<TEntity> entity => _dbContext.Set<TEntity>();

        #endregion

        #region Ctor
        public GenericRepository(CustomerAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await this.entity.AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {

            bool response = false;
            try
            {
                var customer = await GetByIdAsync(id);
                if (customer != null)
                {
                    customer.IsDelete = true;
                    var result = await UpdateAsync(customer);
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

        public IEnumerable<TEntity> GetAll()
        {
            return this.entity.AsEnumerable();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await this.entity.FindAsync(id);
        }

        public Task<List<TEntity>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}

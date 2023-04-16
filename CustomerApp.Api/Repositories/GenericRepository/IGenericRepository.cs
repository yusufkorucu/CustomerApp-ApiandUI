using CustomerApp.Api.Model.Base;

namespace CustomerApp.Api.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetListAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<int> CompleteAsync();
        IEnumerable<TEntity> GetAll();
    }
}

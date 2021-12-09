using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AwesomeBlog.Core.Entities.Common;

namespace AwesomeBlog.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken);

        Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        
        Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
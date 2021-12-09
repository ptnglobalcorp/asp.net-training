using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AwesomeBlog.Core.Entities.Common;
using AwesomeBlog.Core.Interfaces;

namespace AwesomeBlog.Infrastructure.Persistence
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly ICurrentUserService CurrentUserService;

        protected BaseRepository(ICurrentUserService currentUserService)
        {
            CurrentUserService = currentUserService;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            SetCreationAuditedFields(entity);
            return await CreateEntityAsync(entity, cancellationToken);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            SetModificationAuditedFields(entity);
            return await UpdateEntityAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                if (entity is ISoftDelete softDeleteEntity)
                {
                    softDeleteEntity.IsDeleted = true;
                    await UpdateEntityAsync(entity, cancellationToken);
                    return true;
                }

                return false;
            }

            return await DeleteEntityAsync(entity, cancellationToken);
        }

        public abstract Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken);

        public abstract Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken);

        protected abstract Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken);
        protected abstract Task<TEntity> UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken);
        protected abstract Task<bool> DeleteEntityAsync(TEntity entity, CancellationToken cancellationToken);

        protected void SetCreationAuditedFields(TEntity entity)
        {
            if (typeof(IDateEntity).IsAssignableFrom(typeof(TEntity)))
            {
                if (entity is IDateEntity dateEntity)
                {
                    dateEntity.CreatedAt = DateTime.UtcNow;
                }
            }

            if (typeof(IAuditedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                if (entity is IAuditedEntity auditedEntity)
                {
                    auditedEntity.CreatedBy = CurrentUserService.UserId;
                }
            }
        }

        protected void SetModificationAuditedFields(TEntity entity)
        {
            if (typeof(IDateEntity).IsAssignableFrom(typeof(TEntity)))
            {
                if (entity is IDateEntity dateEntity)
                {
                    dateEntity.LastModifiedAt = DateTime.UtcNow;
                }
            }

            if (typeof(IAuditedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                if (entity is IAuditedEntity auditedEntity)
                {
                    auditedEntity.LastModifiedBy = CurrentUserService.UserId;
                }
            }
        }
    }
}
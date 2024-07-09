using System.Linq.Expressions;
using ChatApp.Domain.Abstraction;
using ChatApp.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
    internal abstract class Repository<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>
        where TEntityId : class
    {
        protected readonly ApplicationDbContext dbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity?> GetByIdAsync(
            TEntityId id,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }

        public virtual void Add(TEntity entity)
        {
            this.dbContext.Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            this.dbContext.Remove(entity);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken = default
        )
        {
            return await this
                .dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(expression)
                .AnyAsync(cancellationToken);
        }

        protected IQueryable<TEntity> ApplySpecification(
            Specification<TEntity, TEntityId> specification
        )
        {
            return SpecificationEvaluator.GetQuery(this.dbContext.Set<TEntity>(), specification);
        }
    }
}

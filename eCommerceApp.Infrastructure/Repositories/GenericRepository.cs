using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Application.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace eCommerceApp.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext dbContext) : IGeneric<TEntity> where TEntity : class
    {
        public async Task<int> AddAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await dbContext.Set<TEntity>().FindAsync(id); 
            if(entity is null)
                return 0;
           

            dbContext.Set<TEntity>().Remove(entity);
            return await dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
           var result = await dbContext.Set<TEntity>().FindAsync(id);
           
           return result!;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}

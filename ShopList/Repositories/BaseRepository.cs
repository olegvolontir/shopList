using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopList.Models.Database;
using ShopList.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ShopList.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly ShopDbContext DbContext;
        protected readonly DbSet<T> Table;

        public BaseRepository(ShopDbContext dbContext)
        {
            DbContext = dbContext;
            Table = DbContext.Set<T>();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return await Table
                    .Where(predicate)
                    .FirstOrDefaultAsync();

            return await Table.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return await Table
                    .Where(predicate)
                    .ToListAsync();

            return await Table.ToListAsync();
        }

        public async Task<int> CountAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return await Table
                    .Where(predicate)
                    .CountAsync();

            return await Table.CountAsync();
        }

        public async Task Commit()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task<T> Create(T entity, bool commit = true)
        {
            await Table.AddAsync(entity);

            if (commit)
                await Commit();

            return entity;
        }

        public async Task<T> Update(T entity, bool commit = true)
        {
            Table.Update(entity);   

            if (commit)
                await Commit();

            return entity;
        }

        public async Task<T> Delete(T entity, bool commit = true)
        {
            Table.Remove(entity);

            if (commit)
                await Commit();

            return entity;
        }
    }
}

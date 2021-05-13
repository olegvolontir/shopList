using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShopList.Repositories;
using ShopList.Models.Database.Entities;

namespace ShopList.Services
{
    public class BaseService<T> where T : BaseEntity
    {
        protected BaseRepository<T> BaseRepository;

        public BaseService(BaseRepository<T> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return await BaseRepository.Get(predicate);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return await BaseRepository.GetAll(predicate);
        }

        public async Task<int> CountAll(Expression<Func<T, bool>> predicate = null)
        {
            return await BaseRepository.CountAll(predicate);
        }

        public async Task Commit()
        {
            await BaseRepository.Commit();
        }

        public async Task<T> Create(T entity, bool commit = true)
        {

            return await BaseRepository.Create(entity, commit);
        }

        public async Task<T> Update(T entity, bool commit = true)
        {
            entity.DateModified = DateTime.Now;
            return await BaseRepository.Update(entity, commit);
        }

        public async Task<T> Delete(T entity, bool commit = true)
        {
            return await BaseRepository.Delete(entity, commit);
        }
    }

}


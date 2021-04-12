using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using ShopList.Models.Database;
using ShopList.Models.Database.Entities;

namespace ShopList.Repositories
{
    public class ProductEntityRepository
    {
        private readonly ShopDbContext _dbContext;

        public ProductEntityRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductEntity>> Search(string text)
        {
            return await _dbContext.Products
                .Where(p => p.Name.Contains(text))
                .ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var product = await GetById(id);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        

        public async Task<ProductEntity> GetById(int id)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async Task<ProductEntity> Update(ProductEntity product)
        {
            var result = _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ProductEntity> Insert(ProductEntity product)
        {
            EntityEntry<ProductEntity> result = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

    }
}

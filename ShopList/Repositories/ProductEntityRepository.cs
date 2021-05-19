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
    public class ProductEntityRepository:BaseRepository<ProductEntity>
    {
        public ProductEntityRepository(ShopDbContext dbContext):base (dbContext)
        {
        }

        public async Task<List<ProductEntity>> Search(string text)
        {
            return await Table
                .Where(p => p.Name.Contains(text))
                .ToListAsync();
        }
    }
}

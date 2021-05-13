﻿using System;
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
        private readonly ShopDbContext _dbContext;

        public ProductEntityRepository(ShopDbContext dbContext):base (dbContext)
        {
        }

        public async Task<List<ProductEntity>> Search(string text)
        {
            return await _dbContext.Products
                .Where(p => p.Name.Contains(text))
                .ToListAsync();
        }
    }
}

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
    public class CartEntityRepository : BaseRepository<CartEntity>
    {
        public CartEntityRepository(ShopDbContext dbContext):base (dbContext)
        {         
        }

        //public async Task<List<CartEntity>> Search(int userId)
        //{
        //    return await Table
        //        .Where(c => c.User.Id == userId)
        //        .ToListAsync();
        //}
    }
}

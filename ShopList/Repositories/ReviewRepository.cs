using ShopList.Models.Database;
using ShopList.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Repositories
{
    public class ReviewRepository : BaseRepository<ReviewEntity>
    {
        public ReviewRepository(ShopDbContext shopDbContext):base(shopDbContext)
        {
        }
    }
}

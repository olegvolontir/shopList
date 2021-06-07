using ShopList.Models.Database.Entities;
using ShopList.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Services
{
    public class ReviewService : BaseService<ReviewEntity>
    {
        public ReviewService(ReviewRepository reviewRepository):base(reviewRepository)
        {
        }
    }
}

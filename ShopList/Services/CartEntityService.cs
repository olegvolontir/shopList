using ShopList.Models.Database.Entities;
using ShopList.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Services
{
    public class CartEntityService : BaseService<CartEntity>
    {
        private readonly CartEntityRepository _cartEntityRepository;

        public CartEntityService(CartEntityRepository cartEntityRepository):base(cartEntityRepository)
        {
            _cartEntityRepository = cartEntityRepository;
        }

        public async Task<List<CartEntity>> Search(int userId)
        {
            return await _cartEntityRepository.Search(userId);
        }
    }
}

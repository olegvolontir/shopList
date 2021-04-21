using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopList.Repositories;
using ShopList.Models.Database.Entities;


namespace ShopList.Services
{
    public class ProductEntityService:BaseService<ProductEntity>
    {
        private readonly ProductEntityRepository _productEntityRepository;

        public ProductEntityService(ProductEntityRepository productEntityRepository):base(productEntityRepository)
        {
            _productEntityRepository = productEntityRepository;
        }

       
        public async Task<List<ProductEntity>> Search(string text)
        {
            return await _productEntityRepository.Search(text);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopList.Repositories;
using ShopList.Models.Database.Entities;


namespace ShopList.Services
{
    public class ProductEntityService
    {
        private readonly ProductEntityRepository _productEntityRepository;

        public ProductEntityService(ProductEntityRepository productEntityRepository)
        {
            _productEntityRepository = productEntityRepository;
        }

        public async Task<ProductEntity> CreateProduct(ProductEntity product)
        {
            ProductEntity result = await _productEntityRepository.Insert(product);
            return result;
        }

        public async Task<List<ProductEntity>> Search(string text)
        {
            return await _productEntityRepository.Search(text);
        }

        public async Task<bool> Delete(int id)
        {
            return await _productEntityRepository.Delete(id);
        }

       
        public async Task<ProductEntity> GetById(int id)
        {
            return await _productEntityRepository.GetById(id);
        }


        public async Task<ProductEntity> Update(ProductEntity product)
        {
            return await _productEntityRepository.Update(product);
        }

    }
}

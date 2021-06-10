using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopList.Repositories;
using ShopList.Models.Database.Entities;
using Microsoft.AspNetCore.Http;
using ShopList.Models.FilterModels;
using Microsoft.EntityFrameworkCore;

namespace ShopList.Services
{
    public class ProductEntityService : BaseService<ProductEntity>
    {
        private readonly ProductEntityRepository _productEntityRepository;
        private readonly ProductFilterParameters _productFilterParameters;


        public ProductEntityService(ProductEntityRepository productEntityRepository, IHttpContextAccessor context) : base(productEntityRepository)
        {
            _productEntityRepository = productEntityRepository;
            _productFilterParameters = (ProductFilterParameters)context.HttpContext.Items["filter"];
        }


        public async Task<List<ProductEntity>> Search(string text)
        {
            return await _productEntityRepository.Search(text);
        }

        public async Task<List<ProductEntity>> GetProducts()
        {
            var result = await _productEntityRepository.Get(p => p.Categories.Select(c => c.Name).Intersect(_productFilterParameters.Categories).Any() == true).ToListAsync();
            return result;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopList.Services;
using ShopList.Models.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ShopList.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductEntityController : ControllerBase
    {
        private readonly ProductEntityService _productEntityService;

        public ProductEntityController(ProductEntityService productEntityService)
        {
            _productEntityService = productEntityService;
        }

        [HttpGet("{productId}")]
        public async Task<ObjectResult> GetById([FromRoute] int productId)
        {
            return Ok(await _productEntityService.Get(p => p.Id == productId));
        }

        [HttpPut]
        public async Task<ObjectResult> UpdateProduct([FromBody] ProductEntity product)
        {
            return Ok(await _productEntityService.Update(product));
        }

        [HttpPost]
        public async Task<ObjectResult> CreateProduct([FromBody] ProductEntity product)
        {
            return Ok(await _productEntityService.Create(product));
        }

        [HttpDelete("{productId}")]
        public async Task<ObjectResult> DeleteProduct([FromRoute] int productId)
        {
            var result = await _productEntityService.Get(p => p.Id == productId);

            return Ok(await _productEntityService.Delete(result));
        }
    }
}

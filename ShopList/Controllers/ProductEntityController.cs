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

        [HttpGet("{listId}")]
        public async Task<ObjectResult> GetById([FromRoute] int productId)
        {
            return Ok(await _productEntityService.GetById(productId));
        }

        [HttpPut]
        public async Task<ObjectResult> UpdateProduct([FromBody] ProductEntity product)
        {
            return Ok(await _productEntityService.Update(product));
        }

        [HttpDelete("{listId}")]
        public async Task<ObjectResult> DeleteProduct([FromRoute] int productId)
        {
            return Ok(await _productEntityService.Delete(productId));
        }

        [HttpPost]
        public async Task<ObjectResult> CreateProduct([FromBody] ProductEntity product)
        {
            return Ok(await _productEntityService.CreateProduct(product));
        }
    }
}

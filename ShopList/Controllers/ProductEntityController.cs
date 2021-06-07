using System.Threading.Tasks;
using ShopList.Services;
using ShopList.Models.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ShopList.Models.Requests;

namespace ShopList.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductEntityController : ControllerBase
    {
        private readonly ProductEntityService _productEntityService;
        private readonly UserService _userService;

        public ProductEntityController(ProductEntityService productEntityService, UserService userService)
        {
            _productEntityService = productEntityService;
            _userService = userService;
        }

        [Authorize(Roles = "Normal")]
        [HttpGet]
        public async Task<ObjectResult> GetProducts()
        {
            return Ok(await _productEntityService.GetAll());
        }

        [Authorize(Roles = "Normal")]
        [HttpGet("{productId}")]
        public async Task<ObjectResult> GetById([FromRoute] int productId)
        {
            return Ok(await _productEntityService.Get(p => p.Id == productId));
        }

        [Authorize(Roles = "Moderator")]
        [HttpPut]
        public async Task<ObjectResult> UpdateProduct([FromBody] ProductEntity product)
        {
            return Ok(await _productEntityService.Update(product));
        }

        [Authorize(Roles ="Administrator")]
        [HttpPost]
        public async Task<ObjectResult> CreateProduct([FromBody] AddProductRequest product)
        {
            ProductEntity newProduct = new()
            { 
                Name = product.Name,
                Price = product.Price
            };
            return Ok(await _productEntityService.Create(newProduct));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{productId}")]
        public async Task<ObjectResult> DeleteProduct([FromRoute] int productId)
        {
            var result = await _productEntityService.Get(p => p.Id == productId);

            return Ok(await _productEntityService.Delete(result));
        }
    }
}

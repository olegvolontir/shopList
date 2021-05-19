using System.Threading.Tasks;
using ShopList.Services;
using ShopList.Models.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ShopList.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartEntityController : ControllerBase
    {
        private readonly CartEntityService _cartEntityService;

        public CartEntityController(CartEntityService cartEntityService)
        {
            _cartEntityService = cartEntityService;
        }

        [HttpGet("{cartId}")]
        public async Task<ObjectResult> GetById([FromRoute] int cartId)
        {
            return Ok(await _cartEntityService.Get(c => c.Id == cartId));
        }

        [HttpPut]
        public async Task<ObjectResult> UpdateCart([FromBody] CartEntity cart)
        {
            return Ok(await _cartEntityService.Update(cart));
        }

        [HttpPost]
        public async Task<ObjectResult> CreateCart([FromBody] CartEntity cart)
        {
            return Ok(await _cartEntityService.Create(cart));
        }

        [HttpDelete("{cartId}")]
        public async Task<ObjectResult> DeleteCart([FromRoute] int cartId)
        {
            var result = await _cartEntityService.Get(c => c.Id == cartId);

            return Ok(await _cartEntityService.Delete(result));
        }
    }
}

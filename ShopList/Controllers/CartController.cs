using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using ShopList.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ShopList.Controllers
{
    [Authorize(Roles = "Normal")]
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartEntityService _cartEntityService;
        private readonly UserService _userService;
        private readonly ProductEntityService _productEntityService;

        public CartController(CartEntityService cartEntityService, UserService userService, ProductEntityService productEntityService)
        {
            _cartEntityService = cartEntityService;
            _userService = userService;
            _productEntityService = productEntityService;
        }

        [HttpGet("GetContent")]
        public async Task<ObjectResult> GetCartContent()
        {
            var user = await _userService.Get(u => u.Id == User.GetUserId())
                .Include(u => u.Cart.Products)
                .FirstOrDefaultAsync();
            var cart = user.Cart;

            return Ok(cart.Products);
        }

        [HttpPut("Buy")]
        public async Task<ObjectResult> EmptyCart()
        {
            var user = await _userService.Get(u => u.Id == User.GetUserId()).Include(u=>u.Cart.Products).FirstOrDefaultAsync();
            var cart = user.Cart;

            cart.Products.Clear();

            return Ok(await _cartEntityService.Update(cart));
        }


        [HttpPut("DeleteProduct/{productId}")]
        public async Task<ObjectResult> DeleteFromCart([FromRoute] int productId)
        {
            var user = await _userService.Get(u => u.Id == User.GetUserId()).Include(u => u.Cart.Products).FirstOrDefaultAsync();
            var cart = user.Cart;

            var product = await _productEntityService.Get(p => p.Id == productId).FirstOrDefaultAsync();

            if (cart.Products.Contains(product))
            {
                cart.Products.Remove(product);
            }

            return Ok(await _cartEntityService.Update(cart));
        }
    }
}

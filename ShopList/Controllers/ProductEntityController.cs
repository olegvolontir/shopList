using System.Threading.Tasks;
using ShopList.Services;
using ShopList.Models.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ShopList.Models.Requests;
using ShopList.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ShopList.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductEntityController : ControllerBase
    {
        private readonly ProductEntityService _productEntityService;
        private readonly CartEntityService _cartEntityService;
        private readonly UserService _userService;

        public ProductEntityController(ProductEntityService productEntityService, CartEntityService cartEntityService, UserService userService)
        {
            _cartEntityService = cartEntityService;
            _userService = userService;
            _productEntityService = productEntityService;
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
            return Ok(await _productEntityService.Get(p => p.Id == productId).FirstOrDefaultAsync());
        }

        [Authorize(Roles = "Moderator")]
        [HttpPut]
        public async Task<ObjectResult> UpdateProduct([FromBody] UpdateProductRequest updateProductRequest)
        {
            ProductEntity product = await _productEntityService.Get(p => p.Id == updateProductRequest.Id).FirstOrDefaultAsync();

            product.Name = updateProductRequest.Name;
            product.Price = updateProductRequest.Price;
            product.Discount = updateProductRequest.Discount;

            return Ok(await _productEntityService.Update(product));
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost("AddProduct")]
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
            var result = await _productEntityService.Get(p => p.Id == productId).FirstOrDefaultAsync();

            return Ok(await _productEntityService.Delete(result));
        }

        [Authorize(Roles ="Normal")]
        [HttpPut("AddToCart/{productId}")]
        public async Task<ObjectResult> AddToCart([FromRoute] int productId)
        {
            var user = await _userService.Get(u => u.Id == User.GetUserId()).Include(u => u.Cart.Products).FirstOrDefaultAsync();
            var cart = user.Cart;
            var product = await _productEntityService.Get(p => p.Id == productId).FirstOrDefaultAsync();

            cart.Products.Add(product);

            return Ok(await _cartEntityService.Update(cart));
        }
    }
}

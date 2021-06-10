using System.Threading.Tasks;
using ShopList.Services;
using ShopList.Models.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ShopList.Models.Requests;
using ShopList.Helpers;
using Microsoft.EntityFrameworkCore;
using ShopList.Filters;
using AutoMapper;
using System.Collections.Generic;
using ShopList.Models.Responses;

namespace ShopList.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductEntityController : ControllerBase
    {
        private readonly ProductEntityService _productEntityService;
        private readonly CartEntityService _cartEntityService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public ProductEntityController
            (
            ProductEntityService productEntityService,
            CartEntityService cartEntityService,
            UserService userService,
            IMapper mapper
            )
        {
            _cartEntityService = cartEntityService;
            _userService = userService;
            _productEntityService = productEntityService;
            _mapper = mapper;
        }
        
        [ProductFilter]
        [Authorize(Roles = "Normal")]
        [HttpGet("Get")]
        public async Task<ObjectResult> GetProducts()
        {
            var q = await _productEntityService.GetProducts();


            var res = _mapper.Map<List<GetProductResponse>>(q);
            return Ok(res);
        }

        [Authorize(Roles = "Normal")]
        [HttpGet("Get/{productId}")]
        public async Task<ObjectResult> GetById([FromRoute] int productId)
        {
            return Ok(_mapper.Map<GetProductResponse>(await _productEntityService.Get(p => p.Id == productId).Include(p => p.Categories).FirstOrDefaultAsync()));
        }

        [Authorize(Roles = "Moderator")]
        [HttpPut("Update")]
        public async Task<ObjectResult> UpdateProduct([FromBody] UpdateProductRequest updateProductRequest)
        {
            ProductEntity product = await _productEntityService.Get(p => p.Id == updateProductRequest.Id).FirstOrDefaultAsync();

            product.Name = !string.IsNullOrEmpty(updateProductRequest.Name) ? updateProductRequest.Name : product.Name;
            product.Price = updateProductRequest.Price == 0 ? product.Price : updateProductRequest.Price;
            product.Discount = updateProductRequest.Discount == 0 ? product.Discount : updateProductRequest.Discount;

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

        [Authorize(Roles = "Moderator")]
        [HttpDelete("Delete/{productId}")]
        public async Task<ObjectResult> DeleteProduct([FromRoute] int productId)
        {
            var result = await _productEntityService.Get(p => p.Id == productId).FirstOrDefaultAsync();

            return Ok(await _productEntityService.Delete(result));
        }

        [Authorize(Roles = "Normal")]
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

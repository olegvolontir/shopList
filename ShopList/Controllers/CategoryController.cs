using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopList.Models.Database.Entities;
using ShopList.Models.Requests;
using ShopList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Controllers
{
    [Authorize(Roles = "Moderator")]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ProductEntityService _productEntityService;

        public CategoryController(CategoryService categoryService, ProductEntityService productEntityService)
        {
            _categoryService = categoryService;
            _productEntityService = productEntityService;
        }

        [HttpGet("GetAll")]
        public async Task<ObjectResult> GetCategories()
        {
            return Ok(await _categoryService.Get().ToListAsync());
        }

        [HttpPost("Add")]
        public async Task<ObjectResult> AddCategory([FromBody] AddCategoryRequest addCategoryRequest)
        {
            if (string.IsNullOrEmpty(addCategoryRequest.Name))
                return BadRequest("The string is null or empty!");

            var category = new CategoryEntity()
            {
                Name = addCategoryRequest.Name
            };
            return Ok(await _categoryService.Create(category));
        }

        [HttpDelete("Delete/{categoryId}")]
        public async Task<ObjectResult> DeleteCategory([FromRoute] int categoryId)
        {
            var category = await _categoryService.Get(c => c.Id == categoryId).FirstOrDefaultAsync();

            return Ok(await _categoryService.Delete(category));
        }

        [HttpPut("AddProduct")]
        public async Task<ObjectResult> AddProductToCategory([FromQuery] int productId, [FromQuery] int categoryId)
        {
            var category = await _categoryService.Get(c => c.Id == categoryId).Include(c => c.Products).FirstOrDefaultAsync();
            var product = await _productEntityService.Get(p => p.Id == productId).FirstOrDefaultAsync();

            category.Products.Add(product);

            return Ok(await _categoryService.Update(category));
        }

        [HttpPut("DeleteProduct")]
        public async Task<ObjectResult> DeleteProductFromCategory([FromQuery] int productId, [FromQuery] int categoryId)
        {
            var category = await _categoryService.Get(c => c.Id == categoryId).Include(c => c.Products).FirstOrDefaultAsync();
            var product = await _productEntityService.Get(p => p.Id == productId).FirstOrDefaultAsync();

            if (category.Products.Contains(product))
            {
                category.Products.Remove(product);
            }

            return Ok(await _categoryService.Update(category));
        }
        
    }
}

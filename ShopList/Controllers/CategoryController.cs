using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopList.Models.Database.Entities;
using ShopList.Models.Requests;
using ShopList.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Controllers
{
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

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public async Task<ObjectResult> AddCategory([FromBody] AddCategoryRequest addCategoryRequest)
        {
            var category = new CategoryEntity()
            {
                Name = addCategoryRequest.Name
            };

            return Ok(await _categoryService.Create(category));
        }

        [Authorize(Roles = "Moderator")]
        [HttpDelete]
        public async Task<ObjectResult> DeleteCategory([FromRoute] int categoryId)
        {
            var category = await _categoryService.Get(c => c.Id == categoryId);

            return Ok(await _categoryService.Delete(category));
        }

        [Authorize(Roles = "Moderator")]
        [HttpPut]
        public async Task<ObjectResult> AddProductToCategory([FromQuery]int productId, [FromQuery]int categoryId)
        {
            var category = _categoryService.Get(c => c.Id == categoryId).Result;
            var product = _productEntityService.Get(p=>p.Id == productId).Result;

            if (category.Products == null)
                category.Products = new();

            category.Products.Add(product);

            return Ok(await _categoryService.Update(category));
        }
    }
}

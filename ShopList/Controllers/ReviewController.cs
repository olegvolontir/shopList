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
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        private readonly ProductEntityService _productEntityService;

        public ReviewController(ReviewService reviewService, ProductEntityService productEntityService)
        {
            _reviewService = reviewService;
            _productEntityService = productEntityService;
        }

        [Authorize(Roles = "Normal,Moderator")]
        [HttpGet("{productId}")]
        public async Task<ObjectResult> GetReviews([FromRoute] int productId)
        {
            return Ok(await _reviewService.Get(r => r.Product.Id == productId).ToListAsync());
        }

        [Authorize(Roles = "Normal")]
        [HttpDelete("{reviewId}")]
        public async Task<ObjectResult> DeleteReview([FromRoute] int reviewId)
        {
            var product = await _reviewService.Get(r => r.Id == reviewId).FirstOrDefaultAsync();

            return Ok(await _reviewService.Delete(product));
        }

        [Authorize(Roles = "Normal")]
        [HttpPost]
        public async Task<ObjectResult> PostReview([FromBody] AddReviewRequest reviewRequest)
        {
            ProductEntity product = await _productEntityService.Get(p => p.Id == reviewRequest.ProductId).FirstOrDefaultAsync();

            var review = new ReviewEntity()
            {
                Product = product,
                Content = reviewRequest.Content,
                Rating = reviewRequest.Rating
            };

            return Ok(await _reviewService.Create(review));
        }
    }
}

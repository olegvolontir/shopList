﻿using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Normal")]
        [HttpGet("{productId}")]
        public async Task<ObjectResult> GetReviews([FromRoute] int productId)
        {
            return Ok(await _reviewService.GetAll(r => r.Product.Id == productId));
        }

        [Authorize(Roles = "Normal")]
        [HttpDelete("{reviewId}")]
        public async Task<ObjectResult> DeleteReview([FromRoute] int reviewId)
        {
            var product = await _reviewService.Get(r => r.Id == reviewId);

            return Ok(await _reviewService.Delete(product));
        }

        [Authorize(Roles = "Normal")]
        [HttpPost]
        public async Task<ObjectResult> PostReview([FromBody] AddReviewRequest reviewRequest)
        {
            ProductEntity product = _productEntityService.Get(p => p.Id == reviewRequest.ProductId).Result;

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
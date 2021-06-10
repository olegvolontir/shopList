using Microsoft.AspNetCore.Mvc.Filters;
using ShopList.Models.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Filters
{
    public class ProductFilter : Attribute,IResourceFilter 
    {
        
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var query = context.HttpContext.Request.Query;
            ProductFilterParameters param = new ProductFilterParameters();
            if (query["minPrice"].ToString()!=null 
                && int.TryParse(query["minPrice"].ToString(),out var minPrice) 
                && minPrice>=0)
            {
                param.MinPrice = minPrice;
            }

            if (query["maxPrice"].ToString() != null
                && int.TryParse(query["maxPrice"].ToString(), out var maxPrice)
                && maxPrice >=param.MinPrice)
            {
                param.MaxPrice = maxPrice;
            }

            if (query["category"].ToList() != null  )
            {
                var categories = query["category"].ToList();
                param.Categories = categories;
            }
            context.HttpContext.Items.Add("filter", param);
        }
    }
}

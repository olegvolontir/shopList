using ShopList.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.FilterModels
{
    public class ProductFilterParameters
    {
        public int MinPrice { get; set; } = 0;

        public int MaxPrice { get; set; }

        public List<string> Categories { get; set; }
    }
}

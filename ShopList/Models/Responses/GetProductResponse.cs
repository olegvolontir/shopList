using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Responses
{
    public class GetProductResponse
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public List<string> Categories { get; set; }
    }
}

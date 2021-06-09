using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Requests
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
    }
}

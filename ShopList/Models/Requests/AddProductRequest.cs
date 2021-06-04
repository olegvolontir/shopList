using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Requests
{
    public class AddProductRequest
    {
        public string Name { get; set; }
        public float Price { get; set; }
    }
}

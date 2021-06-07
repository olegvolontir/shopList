using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Requests
{
    public class AddReviewRequest
    {
        public int ProductId { get; set; }
        public string Content { get; set; }
        public float Rating { get; set; }
    }
}

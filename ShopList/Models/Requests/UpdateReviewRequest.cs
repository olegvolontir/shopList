using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Requests
{
    public class UpdateReviewRequest
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public float Rating { get; set; }
    }
}

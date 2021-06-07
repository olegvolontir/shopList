using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Responses
{
    public class GetUserResponse
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
        public int Id { get; set; }
    }
}

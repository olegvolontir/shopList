using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Requests
{
    public class UserRegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

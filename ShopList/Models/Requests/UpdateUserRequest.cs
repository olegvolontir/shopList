using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopList.Models.Database.Entities;

namespace ShopList.Models.Requests
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }

    }
}

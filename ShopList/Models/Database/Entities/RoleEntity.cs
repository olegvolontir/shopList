using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ShopList.Models.Database.Entities
{
    public class RoleEntity : IdentityRole<int>
    {
        public List<UserRoleEntity> UserRoles { get; set; }
    }
}

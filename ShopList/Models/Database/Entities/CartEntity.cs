using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Database.Entities
{
    [Table("cart")]
    public class CartEntity:BaseEntity
    {
        [ForeignKey("UserId")]public UserEntity User { get; set; }
        public List<CartProductEntity> CartProducts { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopList.Models.Database.Entities
{
    [Table("product")]
    public class ProductEntity:BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public List<CartProductEntity> CartProducts { get; set; }
    }
}

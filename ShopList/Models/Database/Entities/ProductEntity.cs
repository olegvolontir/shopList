using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopList.Models.Database.Entities
{
    [Table("product")]
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public List<CartEntity> Carts { get; set; }
        public List<CategoryEntity> Categories { get; set; }
    }
}

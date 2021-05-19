using System.ComponentModel.DataAnnotations.Schema;

namespace ShopList.Models.Database.Entities
{
    [Table("cart_product")]
    public class CartProductEntity : BaseEntity
    {
        [ForeignKey("CartId")] public CartEntity Cart { get; set; }
        [ForeignKey("ProductId")] public ProductEntity Product { get; set; }
    }
}

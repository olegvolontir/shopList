using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Database.Entities
{
    [Table("review")]
    public class ReviewEntity : BaseEntity
    {
        public string Content { get; set; }
        public float Rating { get; set; }
        [ForeignKey("ProductId")] public ProductEntity Product { get; set; }
    }
}

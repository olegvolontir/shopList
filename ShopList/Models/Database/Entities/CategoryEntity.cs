using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Database.Entities
{
    [Table("category")]
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }

        public List<ProductEntity> Products { get; set; }
    }
}

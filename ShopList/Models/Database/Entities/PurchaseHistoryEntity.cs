using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Models.Database.Entities
{
    [Table("search_history")]
    public class PurchaseHistoryEntity : BaseEntity
    {
        public string ProductData { get; set; }
        [ForeignKey("UserId")]public UserEntity User { get; set; }
    }
}

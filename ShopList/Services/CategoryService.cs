using ShopList.Models.Database.Entities;
using ShopList.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopList.Services
{
    public class CategoryService : BaseService<CategoryEntity>
    {
        public CategoryService(CategoryRepository categoryRepository):base(categoryRepository)
        {
        }
    }
}

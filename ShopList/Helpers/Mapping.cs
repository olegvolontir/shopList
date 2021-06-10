using AutoMapper;
using ShopList.Models.Database.Entities;
using ShopList.Models.Responses;
using System.Collections.Generic;
using System.Linq;

namespace ShopList.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserEntity, GetUserResponse>();
            CreateMap<CategoryEntity, string>().ConvertUsing(s => s.Name);
            CreateMap<ProductEntity, GetProductResponse>();
        }
    }
}

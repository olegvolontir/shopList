using AutoMapper;
using ShopList.Models.Database.Entities;
using ShopList.Models.Responses;

namespace ShopList.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserEntity, GetUserResponse>();
        }
    }
}

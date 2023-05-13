using AutoMapper;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer;

public class BLLMappingProfile : Profile
{
    public BLLMappingProfile()
    {
        CreateMap<DailyUserInfoModel, DailyUserInfo>()
            .ReverseMap();

        CreateMap<UserModel, User>()
            .ForMember(dest => dest.DailyUsersInfo, opt => opt.MapFrom(src => src.UserInfo))
            .ReverseMap();

        CreateMap<DishModel, Dish>()
            .ReverseMap();
    }
}

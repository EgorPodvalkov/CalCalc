using AutoMapper;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;
using PresentationLayer.DTOs;

namespace PresentationLayer;

public class PLMappingProfile : Profile
{
    public PLMappingProfile()
    {
        CreateMap<DailyUserInfoDTO, DailyUserInfoModel>()
            .ReverseMap();

        CreateMap<UserDTO, UserModel>()
            .ReverseMap();
        
        CreateMap<DishDTO, DishModel>()
            .ReverseMap();

        CreateMap<DishFilterDTO, DishFilterModel>();

        CreateMap<ChartInfoModel, ChartInfoDTO>();
    }
}

using AutoMapper;
using BusinessLogicLayer.DeserializeModels;
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
            .ReverseMap();

        CreateMap<DishModel, Dish>()
            .ReverseMap();

        CreateMap<DishDeserialized, DishModel>()
            .ForMember(dest => dest.ServingSize, opt => opt
                .MapFrom(src => $"{src.ServingSize}g"))
            .ForMember(dest => dest.TotalFat, opt => opt
                .MapFrom(src => $"{src.TotalFat}g"))
            .ForMember(dest => dest.SaturatedFat, opt => opt
                .MapFrom(src => $"{src.SaturatedFat}g"))
            .ForMember(dest => dest.Carbohydrates, opt => opt
                .MapFrom(src => $"{src.Carbohydrates}g"))
            .ForMember(dest => dest.Protein, opt => opt
                .MapFrom(src => $"{src.Protein}g"));
    }
}

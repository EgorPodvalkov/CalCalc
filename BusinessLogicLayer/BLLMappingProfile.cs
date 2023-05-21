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

        CreateMap<DishModel, ExampleDish>()
            .ReverseMap();

        CreateMap<EatenDish, DishModel>()
            .ForMember(dest => dest.Id, opt => opt
                .MapFrom(src => src.ExampleDish.Id))
            .ForMember(dest => dest.Name, opt => opt
                .MapFrom(src => src.ExampleDish.Name))
            .ForMember(dest => dest.KCalorie, opt => opt
                .MapFrom(src => src.ExampleDish.KCalorie))
            .ForMember(dest => dest.ServingSize, opt => opt
                .MapFrom(src => src.ExampleDish.ServingSize))
            .ForMember(dest => dest.TotalFat, opt => opt
                .MapFrom(src => src.ExampleDish.TotalFat))
            .ForMember(dest => dest.SaturatedFat, opt => opt
                .MapFrom(src => src.ExampleDish.SaturatedFat))
            .ForMember(dest => dest.Carbohydrates, opt => opt
                .MapFrom(src => src.ExampleDish.Carbohydrates))
            .ForMember(dest => dest.Protein, opt => opt
                .MapFrom(src => src.ExampleDish.Protein))
            .ForMember(dest => dest.Recipe, opt => opt
                .MapFrom(src => src.ExampleDish.Recipe))
            .ForMember(dest => dest.Quantity, opt => opt
                .MapFrom(src => src.Quantity));

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

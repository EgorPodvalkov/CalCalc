using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class InjectingBLL
{
    public static void InjectBLL(
        this IServiceCollection services)
    {
        services.AddScoped<IDailyUserInfoService, DailyUserInfoService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDishService, DishService>();
    }
}

using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DbStartUp;

public static class InjectingDAL
{
    public static void InjectDAL(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IDailyUserInfoRepository, DailyUserInfoRepository>();

        services.AddDbContext<CalCalcContext>(options =>
        {
            options.UseSqlServer(
                configuration["ConnectionString"]);
            options.EnableSensitiveDataLogging();
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}

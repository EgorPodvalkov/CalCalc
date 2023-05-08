using BusinessLogicLayer.Services;
using DataAccessLayer.DbStartUp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class Injecting
{
    public static void Inject(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CalCalcContext>(options =>
        {
            options.UseSqlServer(
                configuration["ConnectionString"]);
        });
        services.AddScoped<UserService>();
    }
}

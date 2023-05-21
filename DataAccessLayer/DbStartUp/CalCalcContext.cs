using DataAccessLayer.Entities;
using DataAccessLayer.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbStartUp;

public class CalCalcContext : DbContext
{
    public CalCalcContext(DbContextOptions<CalCalcContext> options) : base(options) { }

    public DbSet<DailyUserInfo> DailyUsersInfo { get; set; }
    public DbSet<ExampleDish> ExampleDishes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DailyUserInfoConfiguration());
        modelBuilder.ApplyConfiguration(new EatenDishConfiguration());
        modelBuilder.ApplyConfiguration(new ExampleDishConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

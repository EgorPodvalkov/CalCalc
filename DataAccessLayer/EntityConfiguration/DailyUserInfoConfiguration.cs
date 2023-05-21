using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfiguration;

public class DailyUserInfoConfiguration : IEntityTypeConfiguration<DailyUserInfo>
{
    public void Configure(EntityTypeBuilder<DailyUserInfo> builder)
    {
        // Id Colunm
        builder.HasIndex(x => x.Id);

        // Date Column
        builder.Property(x => x.Date).IsRequired();

        // KCalorieReal Column
        builder.Property(x => x.KCalorieReal).IsRequired();

        // IdUser Column
        builder.Property(x => x.UserId).IsRequired();

        // One to Many - User to DailyUsersInfo
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.DailyUserInfos)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id);

        // One to Many - DailyUserInfo to EatenDishes
        builder
            .HasMany(x => x.EatenDishes)
            .WithOne(x => x.DailyUserInfo)
            .HasForeignKey(x => x.DailyUserInfoId)
            .HasPrincipalKey (x => x.Id);
    }
}

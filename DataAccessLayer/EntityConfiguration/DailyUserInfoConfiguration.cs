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
        builder.Property(x => x.Id).IsRequired();

        // Date Column
        builder.Property(x => x.Date).IsRequired();

        // KCalorieReal Column
        builder.Property(x => x.KCalorieReal).IsRequired();

        // One to Many - User to DailyUsersInfo
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.DailyUsersInfo)
            .HasForeignKey(x => x.Id)
            .HasPrincipalKey(x => x.Id);

        // Many to Many - DailyUsersInfo to Dishes
        builder
            .HasMany(x => x.Dishes)
            .WithMany(x => x.DailyUsersInfo);
    }
}

﻿using DataAccessLayer.Entities;
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

        // Many to Many - DailyUsersInfo to Dishes
        builder
            .HasMany(x => x.Dishes)
            .WithMany(x => x.DailyUsersInfo);
    }
}

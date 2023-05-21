using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfiguration;

public class ExampleDishConfiguration : IEntityTypeConfiguration<ExampleDish>
{
    public void Configure(EntityTypeBuilder<ExampleDish> builder)
    {
        // Id Column
        builder.HasIndex(x => x.Id);

        // Name Column
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(20);

        // KCalorie Column
        builder.Property(x => x.KCalorie).IsRequired();

        // ServingSize Column
        builder.Property(x => x.ServingSize).IsRequired();
        builder.Property(x => x.ServingSize).HasMaxLength(10);

        // TotalFat Column
        builder.Property(x => x.TotalFat).IsRequired();
        builder.Property(x => x.TotalFat).HasMaxLength(10);

        // SaturatedFat Column
        builder.Property(x => x.SaturatedFat).IsRequired();
        builder.Property(x => x.SaturatedFat).HasMaxLength(10);

        // Carbohydrates Column
        builder.Property(x => x.Carbohydrates).IsRequired();
        builder.Property(x => x.Carbohydrates).HasMaxLength(10);

        // Protein Column
        builder.Property(x => x.Protein).IsRequired();
        builder.Property(x => x.Protein).HasMaxLength(10);

        // One to Many - ExampleDish to EatenDishes
        builder
            .HasMany(x => x.EatenDishes)
            .WithOne(x => x.ExampleDish)
            .HasForeignKey(x => x.ExampleDishId)
            .HasPrincipalKey(x => x.Id);
    }
}

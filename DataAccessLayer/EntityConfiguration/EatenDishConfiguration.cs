using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfiguration;

public class EatenDishConfiguration : IEntityTypeConfiguration<EatenDish>
{
    public void Configure(EntityTypeBuilder<EatenDish> builder)
    {
        // Id Column
        builder.HasIndex(x => x.Id);

        // Quantity Column
        builder.Property(x => x.Quantity).IsRequired();
    }
}

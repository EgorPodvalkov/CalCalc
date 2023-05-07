using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Id Column
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        // Ip Column
        builder.Property(x => x.Ip).IsRequired();
    }
}

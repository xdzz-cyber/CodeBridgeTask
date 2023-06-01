using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.Property(d => d.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(d => d.Color)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(d => d.TailLength)
            .IsRequired();

        builder.Property(d => d.Weight)
            .IsRequired();
    }
}
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("genres");

        builder.HasKey(genre => genre.Id);

        builder.Property(genre => genre.Id)
            .HasColumnName("id");

        builder.Property(genre => genre.Name)
            .HasColumnName("name")
            .IsRequired(false);
    }
}

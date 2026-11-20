using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("authors");

        builder.HasKey(author => author.Id);

        builder.Property(author => author.Id)
            .HasColumnName("id");

        builder.Property(author => author.Fullname)
            .HasColumnName("fullname")
            .IsRequired();

        builder.Property(author => author.Country)
            .HasColumnName("country")
            .IsRequired();

        builder.Property(author => author.BirthYear)
            .HasColumnName("birthyear")
            .IsRequired();
    }
}

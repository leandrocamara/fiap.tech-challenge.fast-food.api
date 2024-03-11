using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using Entities.Products.ProductAggregate;

namespace External.Persistence.Config;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(product => product.Id);

        builder
            .Property(product => product.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(product => product.Category)
            .IsRequired()
            .HasConversion(
                    v => (int)v,
                    v => v
                );

        builder
            .Property(product => product.Price)
            .IsRequired();

        builder
            .Property(product => product.Description)
            .HasMaxLength(200)
            .IsRequired();

        builder
           .Property(p => p.Images)
           .HasColumnType("jsonb")  // Pode usar "json" ou "jsonb" dependendo das suas necessidades
           .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                v => JsonSerializer.Deserialize<List<Image>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
           );
    }
}
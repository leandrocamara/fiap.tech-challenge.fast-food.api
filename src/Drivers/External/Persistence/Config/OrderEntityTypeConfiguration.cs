using Entities.Orders.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace External.Persistence.Config;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(order => order.Id);

        builder
            .Property(order => order.CreatedAt)
            .IsRequired();

        builder
            .Property(order => order.TotalPrice)
            .IsRequired();

        builder
            .Property(order => order.Status)
            .HasConversion(
                status => (short)status,
                value => value)
        .IsRequired();

        builder
            .HasMany(e => e.OrderItems)
            .WithOne(e => e.Order)
            .HasForeignKey(e => e.OrderId)
            .HasPrincipalKey(e => e.Id);
        
        builder
            .HasOne(order => order.Customer)
            .WithMany()
            .HasForeignKey(order => order.CustomerId)
            .IsRequired(false);
    }
}
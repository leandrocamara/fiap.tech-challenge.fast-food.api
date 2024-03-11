using Entities.Orders.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace External.Persistence.Config;

public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("orderItems");

        builder.HasKey(orderItem => orderItem.Id);

        builder
            .Property(orderItem => orderItem.Quantity)
            .IsRequired();

        builder
            .Property(orderItem => orderItem.TotalPrice)
            .IsRequired();

        builder
            .HasOne(m => m.Product)
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId)
            .IsRequired();
    }
}
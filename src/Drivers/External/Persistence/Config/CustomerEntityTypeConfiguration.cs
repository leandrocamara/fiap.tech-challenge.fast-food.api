using Entities.Customers.CustomerAggregate;
using Entities.Orders.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace External.Persistence.Config;

public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder.HasKey(customer => customer.Id);

        builder
            .Property(customer => customer.Cpf)
            .HasConversion(
                convertToProviderExpression: cpf => cpf.Value,
                convertFromProviderExpression: value => new Cpf(value))
            .HasMaxLength(11)
            .IsRequired();

        builder
            .Property(customer => customer.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(customer => customer.Email)
            .HasConversion(
                convertToProviderExpression: email => email.Value,
                convertFromProviderExpression: value => new Email(value))
            .HasMaxLength(100)
            .IsRequired();
        
        builder
            .HasMany<Order>()
            .WithOne()
            .HasForeignKey(order => order.CustomerId)
            .IsRequired(false);
    }
}
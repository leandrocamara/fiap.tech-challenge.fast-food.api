using Adapters.Gateways.Customers;
using Adapters.Gateways.Notifications;
using Adapters.Gateways.Orders;
using Adapters.Gateways.Payments;
using Adapters.Gateways.Products;
using External.Clients;
using External.Persistence;
using External.Persistence.Migrations;
using External.Persistence.Repositories;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace External.Extensions;

public static class ExternalExtensions
{
    public static IServiceCollection AddExternalDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FastFoodContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IUnitOfWork, FastFoodContext>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<INotificationClient, NotificationClient>();
        services.AddScoped<IPaymentClient, MercadoPagoClient>();

        return services;
    }

    public static void CreateDatabase(this IApplicationBuilder _, IConfiguration configuration)
    {
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("Default"))
                .ScanIn(typeof(Initial).Assembly).For.Migrations().For.EmbeddedResources())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        using (serviceProvider.CreateScope())
        {
            serviceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
        }
    }
}
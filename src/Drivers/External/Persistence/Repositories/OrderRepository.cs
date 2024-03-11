using Adapters.Gateways.Orders;
using Entities.Orders.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace External.Persistence.Repositories;

public sealed class OrderRepository(FastFoodContext context) : BaseRepository<Order>(context), IOrderRepository
{
    public override Order? GetById(Guid id)
    {
        return context.Orders
            .Include(order => order.Customer)
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.Product)
            .FirstOrDefault(order => order.Id == id);
    }

    public int GetNextOrderNumber()
    {
        var today = DateTime.UtcNow.Date;
        return context.Orders.Count(m => m.CreatedAt >= today) + 1;
    }

    public async Task<IEnumerable<Order>> GetOngoingOrders()
    {
        var status = new[] { OrderStatus.Received(), OrderStatus.Preparing(), OrderStatus.Ready() };

        return await context.Orders
            .Include(order => order.Customer)
            .Where(order => status.Contains(order.Status))
            .OrderByDescending(order => order.Status)
            .ThenBy(order => order.CreatedAt)
            .ToListAsync();
    }
}
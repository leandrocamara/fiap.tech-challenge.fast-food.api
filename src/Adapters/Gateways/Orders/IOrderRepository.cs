using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Orders;

public interface IOrderRepository : IRepository<Order>
{
    int GetNextOrderNumber();
    Task<IEnumerable<Order>> GetOngoingOrders();
}
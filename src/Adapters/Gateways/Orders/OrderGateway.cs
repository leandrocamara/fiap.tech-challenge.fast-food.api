using Application.Gateways;
using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Orders;

public class OrderGateway(IOrderRepository repository) : IOrderGateway
{
    public void Save(Order order) => repository.Add(order);

    public void Update(Order order) => repository.Update(order);

    public Order? GetById(Guid id) => repository.GetById(id);

    public int GetNextOrderNumber() => repository.GetNextOrderNumber();

    public Task<IEnumerable<Order>> GetOngoingOrders() => repository.GetOngoingOrders();
}
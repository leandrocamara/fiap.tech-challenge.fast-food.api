using Entities.Orders.OrderAggregate;

namespace Application.Gateways;

public interface IOrderGateway
{
    void Save(Order order);
    void Update(Order order);
    Order? GetById(Guid id);
    int GetNextOrderNumber();
    Task<IEnumerable<Order>> GetOngoingOrders();
}
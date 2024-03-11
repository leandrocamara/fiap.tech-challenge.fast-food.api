using Application.Gateways;
using Entities.Orders.OrderAggregate;

namespace Application.UseCases.Orders;

public interface IUpdateOrderStatusUseCase : IUseCase<Guid, UpdateOrderStatusResponse>;

public class UpdateOrderStatusUseCase(IOrderGateway orderGateway) : IUpdateOrderStatusUseCase
{
    public Task<UpdateOrderStatusResponse> Execute(Guid orderId)
    {
        var order = orderGateway.GetById(orderId);

        if (order is null)
            throw new ApplicationException("Order not found.");
        
        order.UpdateStatus();
        orderGateway.Update(order);

        return Task.FromResult(new UpdateOrderStatusResponse(order));
    }
}

public record UpdateOrderStatusResponse(Guid Id, string Status)
{
    public UpdateOrderStatusResponse(Order order) : this(order.Id, order.Status)
    {
    }
}
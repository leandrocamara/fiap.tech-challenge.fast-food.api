using Adapters.Controllers.Common;
using Application.UseCases.Orders;

namespace Adapters.Controllers;

public interface IOrderController
{
    Task<Result> CreateOrder(CreateOrderRequest request);
    Task<Result> GetOngoingOrders();
    Task<Result> GetOrderById(Guid id);
    Task<Result> UpdateOrderStatus(Guid id);
}

public class OrderController(
    ICreateOrderUseCase createOrderUseCase,
    IGetOngoingOrdersUseCase getOngoingOrdersUseCase,
    IGetOrderByIdUseCase getOrderByIdUseCase,
    IUpdateOrderStatusUseCase updateOrderStatusUseCase) : BaseController, IOrderController
{
    public async Task<Result> CreateOrder(CreateOrderRequest request)
    {
        try
        {
            var response = await Execute(() => createOrderUseCase.Execute(request));
            return Result.Created(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> GetOngoingOrders()
    {
        try
        {
            var response = await Execute(getOngoingOrdersUseCase.Execute);
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> GetOrderById(Guid id)
    {
        try
        {
            var response = await Execute(() => getOrderByIdUseCase.Execute(id));

            return response is null
                ? Result.NotFound()
                : Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> UpdateOrderStatus(Guid id)
    {
        try
        {
            var response = await Execute(() => updateOrderStatusUseCase.Execute(id));
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }
}
using Adapters.Controllers;
using Application.UseCases.Orders;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Routers;

[ApiController]
[Route("api/orders")]
public class OrderRouter(IOrderController controller) : BaseRouter
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "Criar um novo pedido para o cliente (quando informado) e com os produtos informados. O pedido inicialmente nasce com o status PendingPayment.", typeof(CreateOrderResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var result = await controller.CreateOrder(request);
        return HttpResponse(result);
    }

    [HttpGet("ongoing")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna a lista de pedidos em andamento.", typeof(OrderResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> GetOngoingOrders()
    {
        var result = await controller.GetOngoingOrders();
        return HttpResponse(result);
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna o pedido buscando pelo id informado.", typeof(GetOrderByIdResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caso não encontre o pedido informado.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> GetOrderByIdResponse([FromRoute] Guid id)
    {
        var result = await controller.GetOrderById(id);
        return HttpResponse(result);
    }

    [HttpPost("{id:guid}/status")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna o pedido com seu novo status.", typeof(UpdateOrderStatusResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Caso não encontre o pedido informado.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> UpdateOrderStatus([FromRoute] Guid id)
    {
        var result = await controller.UpdateOrderStatus(id);
        return HttpResponse(result);
    }
}
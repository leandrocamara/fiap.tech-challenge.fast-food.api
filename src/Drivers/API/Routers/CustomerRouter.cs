using Adapters.Controllers;
using Application.UseCases.Customers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Routers;

[ApiController]
[Route("api/customers")]
public class CustomerRouter(ICustomerController controller) : BaseRouter
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "Criar um novo consumidor para identificá-lo em pedidos posteriormente", typeof(CreateCustomerResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
    {
        var result = await controller.CreateCustomer(request);
        return HttpResponse(result);
    }

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna o consumidor buscando-o pelo CPF informado.", typeof(GetCustomerByCpfResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caso não encontre consumidor cadastrado pelo CPF informado.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> GetCustomerByCpf([FromQuery] string cpf)
    {
        var result = await controller.GetCustomerByCpf(cpf);
        return HttpResponse(result);
    }
}
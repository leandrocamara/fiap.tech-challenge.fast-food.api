using Adapters.Controllers;
using Application.UseCases.Products;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Routers;

[ApiController]
[Route("api/products")]
public class ProductRouter(IProductController controller) : BaseRouter
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "Criar um novo produto para ser usado nos pedidos posteriormente.", typeof(CreateProductResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var result = await controller.CreateProduct(request);
        return HttpResponse(result);
    }

    [HttpPut]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Atualizar um produto existente na base de dados.", typeof(PutProductResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> UpdateProduct(PutProductRequest request)
    {
        var result = await controller.UpdateProduct(request);
        return HttpResponse(result);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status202Accepted, "Excluir um produto existente na base de dados.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        var result = await controller.DeleteProduct(id);
        return HttpResponse(result);
    }

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna a lista de produtos da categoria informada no parâmetro.", typeof(GetProductsByCategoryResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caso não encontre nenhum produto na categoria informada.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> GetProductsByCategory([FromQuery] int categoryId)
    {
        var result = await controller.GetProductsByCategory(categoryId);
        return HttpResponse(result);
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna o produto pelo id informado no parâmetro.", typeof(GetProductByIdResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caso não encontre o produto informado.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> GetProductsByIdResponse([FromRoute] Guid id)
    {
        var result = await controller.GetProductById(id);
        return HttpResponse(result);
    }
}
using Application.Gateways;
using Entities.SeedWork;

namespace Application.UseCases.Products;

public interface IDeleteProductUseCase : IUseCase<Guid, DeleteProductResponse>;

public sealed class DeleteProductUseCase(IProductGateway productGateway) : IDeleteProductUseCase
{  
    public Task<DeleteProductResponse> Execute(Guid id)
    {
        try
        {
            var product = productGateway.GetById(id);

            if (product is null)
                throw new ApplicationException("Product not found");

            productGateway.Delete(product);

            return Task.FromResult(new DeleteProductResponse(product.Id));
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover the customer. Error: {e.Message}", e);
        }
    }
}

public record DeleteProductResponse(Guid Id);
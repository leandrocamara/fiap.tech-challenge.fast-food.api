using Application.Gateways;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;

namespace Application.UseCases.Products;

public interface IGetProductByIdUseCase : IUseCase<Guid, GetProductByIdResponse?>;

public sealed class GetProductByIdUseCase(IProductGateway productGateway) : IGetProductByIdUseCase
{
    public async Task<GetProductByIdResponse?> Execute(Guid id)
    {
        try
        {
            var product = productGateway.GetById(id);

            if (product is null) return null;

            return new GetProductByIdResponse(
                product.Id,
                product.Name,
                product.Category.ToString(),
                product.Price,
                product.Description,
                product.Images);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover product. Error: {e.Message}", e);
        }
    }
}

public record GetProductByIdResponse(Guid Id, string Name, string Category, decimal Price, string Description, List<Image> images);
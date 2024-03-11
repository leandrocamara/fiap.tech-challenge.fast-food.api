using Application.Gateways;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;

namespace Application.UseCases.Products;

public interface IGetProductsByCategoryUseCase : IUseCase<int, IEnumerable<GetProductsByCategoryResponse>>;

public sealed class GetProductsByCategoryUseCase(IProductGateway productGateway) : IGetProductsByCategoryUseCase
{
    public async Task<IEnumerable<GetProductsByCategoryResponse>> Execute(int categoryId)
    {
        try
        {
            var products = await productGateway.GetByCategory(categoryId);

            if (!products.Any())
                throw new ApplicationException("Products not found");

            return products.Select(product => new GetProductsByCategoryResponse(
                product.Id,
                product.Name,
                product.Category.ToString(),
                product.Price,
                product.Description,
                product.Images));
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover products. Error: {e.Message}", e);
        }
    }
}

public record GetProductsByCategoryResponse(Guid Id, string Name, string Category, decimal Price, string Description, List<Image> images);
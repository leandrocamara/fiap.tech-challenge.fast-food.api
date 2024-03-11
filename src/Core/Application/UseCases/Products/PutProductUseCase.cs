using Application.Gateways;
using Application.UseCases.Products.Validators;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;

namespace Application.UseCases.Products;

public interface IPutProductUseCase : IUseCase<PutProductRequest, PutProductResponse>;

public sealed class PutProductUseCase(IProductGateway productGateway) : IPutProductUseCase
{
    private readonly ProductPutValidator _validator = new(productGateway);

    public async Task<PutProductResponse> Execute(PutProductRequest request)
    {
        try
        {
            var product = new Product(request.Id, request.Name, request.Category, request.Price, request.Description, Image.ConvertToImages(request.images));
            
            await _validator.Validate(request);
            productGateway.Update(product);

            return new PutProductResponse(
                product.Id,               
                product.Name,
                product.Category.ToString(),
                product.Price,
                product.Description,
                product.Images);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to update the product. Error: {e.Message}", e);
        }
    }
}

public record PutProductRequest(Guid Id, string Name, int Category, decimal Price, string Description, List<string> images);

public record PutProductResponse(Guid Id, string Name, string Category, decimal Price, string Description, List<Image> images);
using Application.Gateways;

namespace Application.UseCases.Products.Validators;

public sealed class ProductPutValidator(IProductGateway productGateway)
{
    public async Task Validate(PutProductRequest request)
    {
        if (!await IsValid(request.Name))
            throw new ApplicationException("Name already used");
    }

    private async Task<bool> IsValid(string name) => true;
}
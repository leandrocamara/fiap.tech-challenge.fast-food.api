using Application.Gateways;
using Entities.SeedWork;

namespace Application.UseCases.Customers;

public interface IGetCustomerByCpfUseCase : IUseCase<string, GetCustomerByCpfResponse?>;

public sealed class GetCustomerByCpfUseCase(ICustomerGateway customerGateway) : IGetCustomerByCpfUseCase
{
    public async Task<GetCustomerByCpfResponse?> Execute(string cpf)
    {
        try
        {
            var customer = await customerGateway.GetByCpf(cpf);

            if (customer is null) return null;

            return new GetCustomerByCpfResponse(
                customer.Id,
                customer.Cpf,
                customer.Name,
                customer.Email);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover the customer. Error: {e.Message}", e);
        }
    }
}

public record GetCustomerByCpfResponse(Guid Id, string Cpf, string Name, string Email);
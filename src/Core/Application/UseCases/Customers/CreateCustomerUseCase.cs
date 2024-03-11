using Application.Gateways;
using Application.UseCases.Customers.Validators;
using Entities.Customers.CustomerAggregate;
using Entities.SeedWork;

namespace Application.UseCases.Customers;

public interface ICreateCustomerUseCase : IUseCase<CreateCustomerRequest, CreateCustomerResponse>;

public sealed class CreateCustomerUseCase(ICustomerGateway customerGateway) : ICreateCustomerUseCase
{
    private readonly CustomerCreationValidator _validator = new(customerGateway);

    public async Task<CreateCustomerResponse> Execute(CreateCustomerRequest request)
    {
        try
        {
            var customer = new Customer(request.Cpf, request.Name, request.Email);

            await _validator.Validate(request);
            customerGateway.Save(customer);

            return new CreateCustomerResponse(
                customer.Id,
                customer.Cpf,
                customer.Name,
                customer.Email);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to register the customer. Error: {e.Message}", e);
        }
    }
}

public record CreateCustomerRequest(string Cpf, string Name, string Email);

public record CreateCustomerResponse(Guid Id, string Cpf, string Name, string Email);
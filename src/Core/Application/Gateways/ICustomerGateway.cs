using Entities.Customers.CustomerAggregate;

namespace Application.Gateways;

public interface ICustomerGateway
{
    void Save(Customer customer);
    Customer? GetById(Guid id);
    Task<Customer?> GetByCpf(Cpf cpf);
}
using Entities.Customers.CustomerAggregate;

namespace Adapters.Gateways.Customers;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByCpf(Cpf cpf);
}
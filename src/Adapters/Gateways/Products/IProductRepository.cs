using Entities.Products.ProductAggregate;

namespace Adapters.Gateways.Products;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategory(Category category);
}
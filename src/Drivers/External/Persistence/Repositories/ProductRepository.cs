using Adapters.Gateways.Products;
using Entities.Products.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace External.Persistence.Repositories;

public sealed class ProductRepository(FastFoodContext context) : BaseRepository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetByCategory(Category category) =>
        await context.Products.Where(product => product.Category.Equals(category)).ToListAsync();
}
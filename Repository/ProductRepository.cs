using CoreServices.Models;
using System.Threading.Tasks;

namespace CoreServices.Repository
{
    public class ProductRepository : IRepository
    {
        private readonly ProductDBContext _dbContext;
        public ProductRepository(ProductDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddAsync(Product product)
        {
            var result = await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}

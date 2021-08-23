using CoreServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreServices.Repository
{
    public interface IRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> AddAsync(Product product);
    }
}

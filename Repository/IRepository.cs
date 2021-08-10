using CoreServices.Models;
using System.Threading.Tasks;

namespace CoreServices.Repository
{
    public interface IRepository
    {
        Task<Product> AddAsync(Product product);

    }
}

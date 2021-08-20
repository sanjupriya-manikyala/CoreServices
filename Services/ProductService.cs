using CoreServices.DTO;
using CoreServices.Models;
using CoreServices.Repository;
using System.Threading.Tasks;

namespace CoreServices.Services
{
    public class ProductService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<ProductDTO> AddAsync(ProductDTO product)
        {
            var model = new Product
            { 
                Name = product.Name,
                Price = product.Price
            };
            var result = await _repository.AddAsync(model);
            {
                product.Id = result.Id;
                product.Name = result.Name;
                product.Price = result.Price;
            };
            return product;
        }
    }
}

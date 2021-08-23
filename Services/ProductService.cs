using CoreServices.DTO;
using CoreServices.Models;
using CoreServices.Repository;
using System.Collections.Generic;
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

        public virtual async Task<List<ProductDTO>> GetProductsAsync()
        {
            var products = await _repository.GetProductsAsync();
            List<ProductDTO> result = new List<ProductDTO>();
            foreach(Product product in products)
            {
                result.Add(new ProductDTO()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                });
            }
            return result;
        }
    }
}

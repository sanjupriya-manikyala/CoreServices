using CoreServices.Models;
using CoreServices.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CoreServices.Tests
{
    public class ProductRepositoryTests
    {
        private readonly Mock<ProductDBContext> _dbContext;
        private readonly ProductRepository _productRepository;
        private readonly Mock<DbSet<Product>> _mockProducts;

        public ProductRepositoryTests()
        {
            _dbContext = new Mock<ProductDBContext>();
            _productRepository = new ProductRepository(_dbContext.Object);
            _mockProducts = new Mock<DbSet<Product>>();

        }

        [Fact]
        public async void AddAsync_PassedValidData_Return_OkResult()
        {
            //Arrange
            var product = new Product { Id = 6, Name = "Test Name6", Price = 670 };
            _dbContext.Setup(p => p.Products.Add(product));

            //Act
            var data = await _productRepository.AddAsync(product);

            //Assert
            Assert.Equal(product, data);
        }
    }
}

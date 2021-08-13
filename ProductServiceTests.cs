using CoreServices.DTO;
using CoreServices.Models;
using CoreServices.Repository;
using CoreServices.Services;
using Moq;
using Xunit;

namespace CoreServices.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IRepository> _repository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _repository = new Mock<IRepository>();
            _productService = new ProductService(_repository.Object);
        }

        [Fact]
        public async void AddAsync_PassedValidData_Return_OkResult()
        {
            //Arrange
            var dto = new ProductDTO() { Id = 6, Name = "Test Name6", Price = 670 };
            var product = new Product { Id = dto.Id, Name = dto.Name, Price = dto.Price };
            _repository.Setup(p => p.AddAsync(product)).ReturnsAsync(product);

            //Act
            var data = await _productService.AddAsync(dto);

            //Assert
            Assert.Equal(dto, data);
        }
    }
}

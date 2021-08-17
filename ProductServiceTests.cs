using AutoFixture;
using CoreServices.DTO;
using CoreServices.Models;
using CoreServices.Repository;
using CoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            var dto = fixture.Create<ProductDTO>();
            _repository.Setup(p => p.AddAsync(product)).ReturnsAsync(product);

            //Act
            var data = await _productService.AddAsync(dto);

            //Assert
            Assert.IsType<CreatedAtActionResult>(data);
        }

        [Fact]
        public async void AddAsync_PassedValidData_Return_MatchResult()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            var dto = fixture.Create<ProductDTO>();
            _repository.Setup(p => p.AddAsync(product)).ReturnsAsync(product);

            //Act
            var data = await _productService.AddAsync(dto);

            //Assert
            Assert.Equal(dto, data);
        }

        [Fact]
        public async void AddAsync_PassedValidData_Return_Exception()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            var dto = fixture.Create<ProductDTO>();
            _repository.Setup(p => p.AddAsync(product)).ReturnsAsync(product);

            //Act
            var data = await _productService.AddAsync(dto);
            data = null;

            //Assert
            var resultType = Assert.IsType<CreatedAtActionResult>(data);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, resultType.StatusCode);
        }
    }
}

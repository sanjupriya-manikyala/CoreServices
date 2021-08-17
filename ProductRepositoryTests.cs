using AutoFixture;
using CoreServices.Models;
using CoreServices.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            _dbContext.Setup(p => p.Products.Add(product));

            //Act
            var data = await _productRepository.AddAsync(product);

            //Assert
            Assert.IsType<CreatedAtActionResult>(data);
        }

        [Fact]
        public async void AddAsync_PassedValidData_Return_MatchResult()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            _dbContext.Setup(p => p.Products.Add(product));

            //Act
            var data = await _productRepository.AddAsync(product);

            //Assert
            Assert.IsType<CreatedAtActionResult>(data);
            Assert.Equal(product, data);
        }

        [Fact]
        public async void AddAsync_PassedValidData_Return_Exception()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            _dbContext.Setup(p => p.Products.Add(product));

            //Act
            var data = await _productRepository.AddAsync(product : null);

            //Assert
            var resultType = Assert.IsType<StatusCodeResult>(data);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, resultType.StatusCode);
        }
    }
}

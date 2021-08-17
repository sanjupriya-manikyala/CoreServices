using AutoFixture;
using CoreServices.Models;
using CoreServices.Repository;
using Moq;
using Xunit;
using System;

namespace CoreServices.Tests
{
    public class ProductRepositoryTests
    {
        private readonly Mock<ProductDBContext> _dbContext;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _dbContext = new Mock<ProductDBContext>();
            _productRepository = new ProductRepository(_dbContext.Object);
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
            Assert.NotNull(data);
            Assert.IsType<Product>(data);
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
            Assert.NotNull(data);
            Assert.IsType<Product>(data);
            Assert.Equal(product, data);
        }

        [Fact]
        public async void AddAsync_PassedInValidData_Return_BadRequest()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            var exception = fixture.Create<Exception>();
            _dbContext.Setup(p => p.Products.Add(product)).Throws(exception);

            //Act
            var data = await Assert.ThrowsAsync<Exception>(() => _productRepository.AddAsync(product));

            //Assert
            Assert.NotNull(data);
            Assert.IsType<Exception>(data);
            Assert.Equal(exception.Message, data.Message);
        }
    }
}

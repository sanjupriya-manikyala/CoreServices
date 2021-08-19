using AutoFixture;
using CoreServices.Models;
using CoreServices.Repository;
using Moq;
using Xunit;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CoreServices.Tests
{
    public class ProductRepositoryTests
    {
        private readonly Mock<ProductDBContext> _dbContext;
        private readonly ProductRepository _productRepository;
        private readonly Mock<DbSet<Product>> _mockproducts;

        public ProductRepositoryTests()
        {
            _mockproducts = new Mock<DbSet<Product>>(); 
            _dbContext = new Mock<ProductDBContext>();
            _productRepository = new ProductRepository(_dbContext.Object);
        }

        [Fact]
        public async void AddAsync_PassedValidData_Return_OkResult()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            _dbContext.Setup(p => p.AddAsync(product, default)).ReturnsAsync(product);

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
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var product = fixture.Create<Product>();
            _dbContext.Setup(p => p.AddAsync(product,default)).ReturnsAsync(product);


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
            _dbContext.Setup(p => p.AddAsync(product, default)).ThrowsAsync(exception);

            //Act
            var data = await Assert.ThrowsAsync<Exception>(() => _productRepository.AddAsync(product));

            //Assert
            Assert.NotNull(data);
            Assert.IsType<Exception>(data);
            Assert.Equal(exception.Message, data.Message);
        }
    }
}

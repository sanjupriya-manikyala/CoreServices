using AutoFixture;
using CoreServices.Models;
using CoreServices.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CoreServices.Tests
{
    public class ProductRepositoryTests
    {

        [Fact]
        public async Task AddAsync_GivenValidData_ReturnsProduct()
        {
            //Arrange  
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            await context.AddAsync(product);

            //Act
            var _productRepository = new ProductRepository(context);
            var result = await _productRepository.AddAsync(product);

            //Assert
            Assert.NotNull(product);
            Assert.IsAssignableFrom<Product>(result);
            Assert.Equal(product, result);
        }

        [Fact]
        public async Task AddAsync_GivenValidData_ReturnsException()
        {
            //Arrange  
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();
            var fixture = new Fixture();
            var product = fixture.Create<Product>();
            await context.AddAsync(product);
            await Assert.ThrowsAsync<Exception>(() => context.SaveChangesAsync());

            //Act
            var _productRepository = new ProductRepository(context);
            var result = await Assert.ThrowsAsync<Exception>(() => _productRepository.AddAsync(product));

            //Assert
            Assert.NotNull(product);
            Assert.IsType<Exception>(result);
        }
    }
}
            
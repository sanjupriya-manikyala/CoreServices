using AutoFixture;
using CoreServices.Models;
using CoreServices.Repository;
using FluentAssertions;
using System.Collections.Generic;
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
        public async Task GetProductsAsync_ReturnsProduct()
        {
            //Arrange  
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();
            var fixture = new Fixture();
            var products = fixture.Create<List<Product>>();
            foreach(Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();

            //Act
            var _productRepository = new ProductRepository(context);
            var result = await _productRepository.GetProductsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            result.Should().BeEquivalentTo(products);
        }
    }
}
            
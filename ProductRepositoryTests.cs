using AutoFixture;
using CoreServices.Models;
using Xunit;

namespace CoreServices.Tests
{
    public class ProductRepositoryTests
    {

        [Fact]
        public void Add_ValidDataPassed_Return_MatchResult()
        {
            //Arrange  
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();
            var fixture = new Fixture();
            var product = fixture.Create<Product>();

            //Act  
            var data = context.Products.Add(product);
            context.SaveChanges();

            //Assert
            Assert.NotNull(data);
            Assert.Equal(product, data.Entity);
        }
    }
}
            
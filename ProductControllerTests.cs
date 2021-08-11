using CoreServices.Controllers;
using CoreServices.DTO;
using CoreServices.Models;
using CoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CoreServices.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<ProductService> _mockProductService;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<ProductService>();
            _productController = new ProductController(_mockProductService.Object);
        }


        [Fact]
        public async void Task_AddAsync_ValidData_Return_OkResult()
        {
            //Arrange
            var product = new ProductDTO() { Id = 5, Name = "Test Name5", Price = 8120 };

            //Act  
            var data = await _productController.AddAsync(product);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
            
        }

        [Fact]
        public async void Task_AddAsync_ValidData_Return_Exception()
        {
            //Arrange
            var product = new ProductDTO() { Id = 5, Name = "Test Name5", Price = 8120 };
            _productController.StatusCode(StatusCodes.Status500InternalServerError);

            //Act  
            var data = await _productController.AddAsync(product) as CreatedAtActionResult;

            //Assert  
            Assert.Equal(StatusCodes.Status500InternalServerError, data.StatusCode);
        }

        [Fact]
        public async void Task_AddAsync_ValidData_Return_MatchResult()
        {
            //Arrange
            var product = new ProductDTO() { Id = 5, Name = "Test Name5", Price = 8120 };

            //Act  
            var data = await _productController.AddAsync(product) as CreatedAtActionResult;
            var value = data.Value as ProductDTO;

            //Assert  
            Assert.Equal(product, value);
        }

        [Fact]
        public void Add_ValidDataPassed_Return_MatchResult()
        {
            //Arrange  
            var factory = new ConnectionFactory();
            var context = factory.CreateContextForInMemory();

            var product = new Product() { Id = 5, Name = "Test Name3", Price = 7150 };

            //Act  
            var data = context.Products.Add(product);
            context.SaveChanges();

            //Assert
            Assert.Equal(product, data.Entity);
        }
    }
}

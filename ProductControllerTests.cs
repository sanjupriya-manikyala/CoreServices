using CoreServices.Controllers;
using CoreServices.DTO;
using CoreServices.Repository;
using CoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;

namespace CoreServices.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<IRepository> _repository;
        private readonly Mock<ProductService> _mockProductService;

        public ProductControllerTests()
        {
            _repository = new Mock<IRepository>();
            _mockProductService = new Mock<ProductService>(_repository.Object);
            _productController = new ProductController(_mockProductService.Object);
        }


        [Fact]
        public async void Task_AddAsync_PassedValidData_Return_OkResult()
        {
            //Arrange
            var product = new ProductDTO() { Id = 5, Name = "Test Name5", Price = 8120 };
            _mockProductService.Setup(p => p.AddAsync(product)).ReturnsAsync(product);

            //Act  
            var data = await _productController.AddAsync(product);

            //Assert
            Assert.IsType<CreatedAtActionResult>(data);
            Assert.IsAssignableFrom<CreatedAtActionResult>(data);
        }

        [Fact]
        public async void Task_AddAsync_PassedValidData_Return_MatchResult()
        {
            //Arrange
            var product = new ProductDTO() { Id = 5, Name = "Test Name5", Price = 8120 };
            _mockProductService.Setup(p => p.AddAsync(product)).ReturnsAsync(product);

            //Act  
            var data = await _productController.AddAsync(product) as CreatedAtActionResult;
            var value = data.Value as ProductDTO;

            //Assert
            Assert.NotNull(value);
            Assert.Equal(product, value);
        }

        [Fact]
        public async void Task_AddAsync_PassedInValidData_Return_BadRequest()
        {
            //Arrange
            var product = new ProductDTO() { Id = 5, Name = "Test Name5", Price = 8120 };
            _mockProductService.Setup(p => p.AddAsync(product)).ReturnsAsync(product);
            _productController.StatusCode(StatusCodes.Status500InternalServerError);

            //Act  
            var data = await _productController.AddAsync(product : null);

            //Assert
            var resulttype = Assert.IsType<StatusCodeResult>(data);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, resulttype.StatusCode);

        }
    }
}

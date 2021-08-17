using AutoFixture;
using CoreServices.Controllers;
using CoreServices.DTO;
using CoreServices.Repository;
using CoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace CoreServices.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly ILogger _logger;
        private readonly Mock<IRepository> _repository;
        private readonly Mock<ProductService> _mockProductService;

        public ProductControllerTests()
        {
            _logger = new Mock<ILogger>().Object;
            _repository = new Mock<IRepository>();
            _mockProductService = new Mock<ProductService>(_repository.Object);
            _productController = new ProductController(_mockProductService.Object, _logger);
        }


        [Fact]
        public async void Task_AddAsync_PassedValidData_Return_OkResult()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<ProductDTO>();
            _mockProductService.Setup(p => p.AddAsync(product)).ReturnsAsync(product);

            //Act  
            var data = await _productController.AddAsync(product) as CreatedAtActionResult;
            var result = data.Value;

            //Assert
            Assert.NotNull(data);
            Assert.IsType<CreatedAtActionResult>(data);
            Assert.Equal(product, result);

            _mockProductService.VerifyAll();
        }

        [Fact]
        public async void Task_AddAsync_PassedValidData_Return_MatchResult()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<ProductDTO>();
            _mockProductService.Setup(p => p.AddAsync(product)).ReturnsAsync(product);

            //Act  
            var data = await _productController.AddAsync(product) as CreatedAtActionResult;
            var value = data.Value as ProductDTO;

            //Assert
            Assert.NotNull(value);
            Assert.IsType<CreatedAtActionResult>(data);
            Assert.Equal(product, value);

            _mockProductService.VerifyAll();
        }

        [Fact]
        public async void Task_AddAsync_PassedInValidData_Return_BadRequest()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<ProductDTO>();
            var exception = fixture.Create<Exception>();
            _mockProductService.Setup(p => p.AddAsync(product)).ThrowsAsync(exception);

            //Act  
            var data = await _productController.AddAsync(product) as ObjectResult;
            var result = data.StatusCode;

            //Assert
            Assert.NotNull(data);
            Assert.Equal(exception.Message, data.Value);
            Assert.Equal(StatusCodes.Status500InternalServerError, result);

            _mockProductService.VerifyAll();
        }

        [Fact]
        public async void Task_AddAsync_PassedInValidData_Return_Exception()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<ProductDTO>();
            _mockProductService.Setup(p => p.AddAsync(product)).ReturnsAsync((ProductDTO)null);

            //Act  
            var data = await _productController.AddAsync(product) as StatusCodeResult;
            var result = data.StatusCode;

            //Assert
            Assert.NotNull(data);
            Assert.IsType<StatusCodeResult>(data);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, result);

            _mockProductService.VerifyAll();
        }
    }
}

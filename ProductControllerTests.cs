using AutoFixture;
using AutoMapper;
using CoreServices.Controllers;
using CoreServices.DTO;
using CoreServices.Profiles;
using CoreServices.Repository;
using CoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoreServices.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly ILogger _logger;
        private readonly Mock<IRepository> _repository;
        private readonly Mock<ProductService> _mockProductService;
        private readonly IMapper _mapper;

        public ProductControllerTests()
        {
            var mappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new ProductProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _logger = new Mock<ILogger>().Object;
            _repository = new Mock<IRepository>();
            _mockProductService = new Mock<ProductService>(_repository.Object, _mapper);
            _productController = new ProductController(_mockProductService.Object, _logger);
        }

        [Fact]
        public async Task Task_AddAsync_PassedValidData_Return_OkResult()
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
        public async Task Task_AddAsync_PassedValidData_Return_MatchResult()
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
        public async Task Task_AddAsync_PassedInValidData_Return_BadRequest()
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
        public async Task Task_AddAsync_PassedInValidData_Return_Exception()
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

        [Fact]
        public async Task GetProductsAsync_ReturnsProducts()
        {
            //Arrange
            var fixture = new Fixture();
            var products = fixture.CreateMany<ProductDTO>().ToList();
            _mockProductService.Setup(p => p.GetProductsAsync()).ReturnsAsync(products);

            //Act
            var data = await _productController.GetProductsAsync() as OkObjectResult;
            var result = data.Value as List<ProductDTO>;

            //Assert
            Assert.NotNull(data);
            Assert.IsType<OkObjectResult>(data);
            Assert.Equal(products, result);
        }

        [Fact]
        public async Task GetProductsAsync_ThrowsException()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.CreateMany<ProductDTO>().ToList();
            var exception = fixture.Create<Exception>();
            _mockProductService.Setup(p => p.GetProductsAsync()).ThrowsAsync(exception);

            //Act  
            var data = await _productController.GetProductsAsync() as ObjectResult;
            var result = data.StatusCode;

            //Assert
            Assert.NotNull(data);
            Assert.Equal(exception.Message, data.Value);
            Assert.Equal(StatusCodes.Status500InternalServerError, result);
            _mockProductService.VerifyAll();
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsNoContent()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.CreateMany<ProductDTO>().ToList();
            _mockProductService.Setup(p => p.GetProductsAsync()).ReturnsAsync((List<ProductDTO>)null);

            //Act  
            var data = await _productController.GetProductsAsync() as StatusCodeResult;
            var result = data.StatusCode;

            //Assert
            Assert.NotNull(data);
            Assert.IsType<NoContentResult>(data);
            Assert.Equal(StatusCodes.Status204NoContent, result);
            _mockProductService.VerifyAll();
        }
    }
}

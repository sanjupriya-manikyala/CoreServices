using AutoFixture;
using AutoFixture.AutoMoq;
using CoreServices.DTO;
using CoreServices.Models;
using CoreServices.Repository;
using CoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace CoreServices.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IRepository> _repository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _repository = new Mock<IRepository>();
            _productService = new ProductService(_repository.Object);
        }

        [Fact]
        public async Task AddAsync_PassedValidData_Return_OkResult()
        {
            //Arrange
            var fixture = new Fixture();
            var dto = fixture.Create<ProductDTO>();
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
            _repository.Setup(p => p.AddAsync(It.Is<Product>(q => q.Name == product.Name))).ReturnsAsync(product);

            //Act
            var data = await _productService.AddAsync(dto);

            //Assert
            Assert.NotNull(data);
            Assert.IsType<ProductDTO>(data);
        }

        [Fact]
        public async Task AddAsync_PassedValidData_Return_MatchResult()
        {
            //Arrange
            var fixture = new Fixture();
            var dto = fixture.Create<ProductDTO>();
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
            _repository.Setup(p => p.AddAsync(It.Is<Product>(q => q.Name == product.Name))).ReturnsAsync(product);

            //Act
            var data = await _productService.AddAsync(dto);

            //Assert
            Assert.NotNull(data);
            Assert.IsType<ProductDTO>(data);
            Assert.Equal(dto, data);
        }

        [Fact]
        public async Task AddAsync_PassedInValidData_Return_BadRequest()
        {
            //Arrange
            var fixture = new Fixture();
            var dto = fixture.Create<ProductDTO>();
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
            var exception = fixture.Create<Exception>();
            _repository.Setup(p => p.AddAsync(It.Is<Product>(q => q.Name == product.Name))).ThrowsAsync(exception);

            //Act
            var data = await Assert.ThrowsAsync<Exception>(() => _productService.AddAsync(dto));

            //Assert
            Assert.NotNull(data);
            Assert.IsType<Exception>(data);
            Assert.Equal(exception.Message, data.Message);
        }

        [Fact]
        public async Task GetProductsAsync_GivenInValidData_ReturnOk()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<List<Product>>();
            List<ProductDTO> dto = new List<ProductDTO>();
            foreach(Product p in product)
            {
                dto.Add(new ProductDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
            }
            _repository.Setup(p => p.GetProductsAsync()).ReturnsAsync(product);

            //Act
            var data = await _productService.GetProductsAsync();

            //Assert
            Assert.NotNull(data);
            Assert.IsType<List<ProductDTO>>(data);
        }

        [Fact]
        public async Task GetProductsAsync_GivenInValidData_ReturnsProducts()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<List<Product>>();
            List<ProductDTO> dto = new List<ProductDTO>();
            foreach (Product p in product)
            {
                dto.Add(new ProductDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
            }
            _repository.Setup(p => p.GetProductsAsync()).ReturnsAsync(product);

            //Act
            var data = await _productService.GetProductsAsync();

            //Assert
            Assert.NotNull(data);
            Assert.IsType<List<ProductDTO>>(data);
            dto.Should().HaveSameCount(data);
            dto.Should().BeEquivalentTo(data);
        }


        [Fact]
        public async Task GetProductsAsync_GivenInValidData_ReturnBadRequest()
        {
            //Arrange
            var fixture = new Fixture();
            var product = fixture.Create<List<Product>>();
            List<ProductDTO> dto = new List<ProductDTO>();
            foreach(Product p in product)
            {
                dto.Add(new ProductDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
            }
            var exception = fixture.Create<Exception>();
            _repository.Setup(p => p.GetProductsAsync()).ThrowsAsync(exception);

            //Act
            var data = await Assert.ThrowsAsync<Exception>(() => _productService.GetProductsAsync());

            //Assert
            Assert.NotNull(data);
            Assert.IsType<Exception>(data);
            Assert.Equal(exception.Message, data.Message);
        }
    }
}

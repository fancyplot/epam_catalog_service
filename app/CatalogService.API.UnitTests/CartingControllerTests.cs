using AutoMapper;
using CatalogService.API.Controllers.V1;
using CatalogService.Domain.Commands.V1.CreateCart;
using CatalogService.Domain.Commands.V1.DeleteCart;
using CatalogService.Domain.Models.V1;
using CatalogService.Domain.Queries.V1.GetCarts;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CatalogService.API.UnitTests
{
    public class CartingControllerTests
    {
        [Fact]
        public async void PostAsync_CreatesACarting_ReturnsOK()
        {
            // Arrange
            int id = 1;
            string name = "test";
            decimal price = 1.28m;
            int quantity = 3;
            string image = "";

            var cart = new Cart()
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Image = image
            };

            var mockMapper = new Mock<IMapper>();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(
                    t => t.Send(
                        It.Is<CreateCartCommand>(q => true),
                        It.Is<CancellationToken>(q => true)))
                .ReturnsAsync(cart);

            var controller = new CategoryController(mockMediator.Object, mockMapper.Object);

            // Act
            var result  = await controller.PostAsync(id, name, price, quantity, image);

            // Assert
            using var scope = new AssertionScope();
            result.Should().BeOfType<CreatedResult>();
            var createdResult = result.As<CreatedResult>();
            createdResult.Location.Should().Be($"v1/carting/{id}");
        }


        [Fact]
        public async void PostAsync_TriesToCreateExistingCarting_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            string name = "test";
            decimal price = 1.28m;
            int quantity = 3;
            string image = "";
            string exception = $"Cart with id {id} already exists";

            var mockMapper = new Mock<IMapper>();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(
                    t => t.Send(
                        It.Is<CreateCartCommand>(q => true),
                        It.Is<CancellationToken>(q => true)))
                .Throws(new ArgumentException(exception));

            var controller = new CategoryController(mockMediator.Object, mockMapper.Object);

            // Act
            var result = await controller.PostAsync(id, name, price, quantity, image);

            // Assert
            using var scope = new AssertionScope();
            result.Should().BeOfType<BadRequestObjectResult>();
            var createdResult = result.As<BadRequestObjectResult>();
            createdResult.Value.Should().Be(exception);
        }

        [Fact]
        public async void GetAsync_GetCarts_ReturnsOk()
        {
            // Arrange
            int id = 1;
            string name = "test";
            decimal price = 1.28m;
            int quantity = 3;
            string image = "";

            var cart = new Cart()
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Image = image
            };

            var mockMapper = new Mock<IMapper>();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(
                    t => t.Send(
                        It.Is<GetCartsQuery>(q => true),
                        It.Is<CancellationToken>(q => true)))
                .ReturnsAsync(new[]{ cart });

            var controller = new CategoryController(mockMediator.Object, mockMapper.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            using var scope = new AssertionScope();
            result.Should().BeOfType<OkObjectResult>();
            var createdResult = result.As<OkObjectResult>();
            createdResult.Value.Should().NotBe(null);
            var cartId = createdResult.Value.As<IEnumerable<Cart>>().First().Id;
            cartId.Should().Be(id);
        }

        [Fact]
        public async void GetAsync_NoCarts_ReturnsNoContent()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(
                    t => t.Send(
                        It.Is<GetCartsQuery>(q => true),
                        It.Is<CancellationToken>(q => true)))
                .ReturnsAsync(new List<Cart>());

            var controller = new CategoryController(mockMediator.Object, mockMapper.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            using var scope = new AssertionScope();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void DeleteAsync_RemoveCart_ReturnsOk()
        {
            // Arrange
            int id = 1;

            var mockMapper = new Mock<IMapper>();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(
                t => t.Send(
                    It.Is<DeleteCartCommand>(q => true),
                    It.Is<CancellationToken>(q => true)));

            var controller = new CategoryController(mockMediator.Object, mockMapper.Object);

            // Act
            var result = await controller.DeleteAsync(id);

            // Assert
            using var scope = new AssertionScope();
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void DeleteAsync_NoCartsToRemove_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            string exception = $"Cart with id {id} does not exist";
            var mockMapper = new Mock<IMapper>();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(
                    t => t.Send(
                        It.Is<DeleteCartCommand>(q => true),
                        It.Is<CancellationToken>(q => true)))
                .Throws(new KeyNotFoundException(exception));

            var controller = new CategoryController(mockMediator.Object, mockMapper.Object);

            // Act
            var result = await controller.DeleteAsync(id);

            // Assert
            using var scope = new AssertionScope();
            result.Should().BeOfType<BadRequestObjectResult>();
            var createdResult = result.As<BadRequestObjectResult>();
            createdResult.Value.Should().Be(exception);
        }
    }
}
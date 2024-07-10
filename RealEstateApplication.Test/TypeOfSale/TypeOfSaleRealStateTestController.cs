using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RealEstateApplication.Application.Dtos.API.TypeOfSale;
using RealEstateApplication.Application.Dtos.TypeOfRealEstate;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Features.TypeOfRealEstates.Commands;
using RealEstateApplication.Application.Features.TypeOfSales.Command.CreateTypeOfSale;
using RealEstateApplication.Application.Features.TypeOfSales.Command.DeleteTypeOfSaleById;
using RealEstateApplication.Application.Features.TypeOfSales.Queries.GetAllTypeOfSale;
using RealEstateApplication.Application.Features.TypeOfSales.Queries.GetAllTypeOfSaleById;
using RealEstateApplication.Application.Wrappers;
using RealEstateApplication.Presentation.WebAPI.Controllers.V1;
using RealEstateApplication.WebApi.Controllers.V1;

namespace RealEstateApplication.Test.TypeOfSale
{
    public class TypeOfSaleRealStateTestController
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TypeOfSaleController _typeOfSaleController;

        public TypeOfSaleRealStateTestController()
        {
            _mediatorMock = new Mock<IMediator>();

            // Configura el HttpContext
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = new ServiceCollection()
                .AddSingleton(_mediatorMock.Object)
                .BuildServiceProvider();

            // Configura el controlador con el HttpContext simulado
            _typeOfSaleController = new TypeOfSaleController(_mediatorMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };
        }


        [Fact]
        public async Task CreateTypeOfSaleStateRealEstate_ShouldReturnStatusCode201()
        {
            //Arrange
            var createTypeOfSaleCommand = new CreateTypeOfSaleCommand
            {
                Name = "Alquilado",
                Description = "Para alquilar"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTypeOfSaleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response<Unit>(Unit.Value));

            //Act
            var result = await _typeOfSaleController.Post(createTypeOfSaleCommand);

            //Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }



        [Fact]
        public async Task CreateTypeOfSaleRealEstate_ShouldReturnBadRequestForNameEmpty()
        {
            // Arrange
            var createTypeOfSaleCommand = new CreateTypeOfSaleCommand
            {
                Name = "",
                Description = ""
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTypeOfSaleCommand>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new ApiException("No pueden haber campos nulos", 400));

            // Act
            var ex = await Assert.ThrowsAsync<ApiException>(() => _typeOfSaleController.Post(createTypeOfSaleCommand));

            // Assert
            Assert.Equal(400, ex.ErrorCode);
        }




        [Fact]
        public async Task GetByIdOfSale_ShouldReturnOkResultWithTypeOfSaleRealEstate()
        {
            // Arrange
            var typeOfSaleRealEstate = new Response<TypeOfSaleDto>
            {
                Succeeded = true,
                Message = "",
                Errors = null,
                Data = new TypeOfSaleDto { Id = 1, Name = "Se vende" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTypeOfSaleByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(typeOfSaleRealEstate);

            // Act
            var result = await _typeOfSaleController.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(typeOfSaleRealEstate, okResult.Value);
        }



        [Fact]
        public async Task GetAllOfSale_ShouldReturnOkResultWithTypeOfSaleRealEstateList()
        {
            // Arrange
            var typeOfSaleRealEstate = new Response<IEnumerable<TypeOfSaleDto>>
            {
                Succeeded = true,
                Message = "",
                Errors = null,
                Data = new List<TypeOfSaleDto>
            {
                new TypeOfSaleDto
                {
                    Id = 1,
                    Name = "Alquilado",
                    Description = "Para alquilar"
                },
                new TypeOfSaleDto
                {
                    Id = 2,
                    Name = "Vender",
                    Description="Para vender"
                },
                new TypeOfSaleDto
                {
                    Id = 3,
                    Name = "Al contrato",
                    Description= "Para el contrato"
                }
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTypeOfSaleQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(typeOfSaleRealEstate);

            // Act
            var result = await _typeOfSaleController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(typeOfSaleRealEstate, okResult.Value);
        }

        [Fact]
        public async Task DeleteOfSale_ShouldReturn204StatusCode_WhenValidRequest()
        {
            // Arrange
            int id = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTypeOfSaleByIdCommand>(), default))
                         .ReturnsAsync(new Response<int>(1));

            // Act
            var result = await _typeOfSaleController.Delete(id);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }
    }
}

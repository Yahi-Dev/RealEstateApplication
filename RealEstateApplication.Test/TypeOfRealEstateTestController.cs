using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstateApplication.Application.Dtos.TypeOfRealEstate;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Features.Improvements.Commands;
using RealEstateApplication.Application.Features.TypeOfRealEstates.Commands;
using RealEstateApplication.Application.Wrappers;
using RealEstateApplication.WebApi.Controllers.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication.Test
{
    public class TypeOfRealEstateTestController
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TypeOfRealEstateController _typeOfRealEstateController;

        public TypeOfRealEstateTestController()
        {
            _mediatorMock = new Mock<IMediator>();
            _typeOfRealEstateController = new TypeOfRealEstateController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateTypeOfRealEstate_ShouldReturnStatusCode201()
        {
            //Arrange
            var createTypeOfRealEstateCommand = new CreateTypeOfRealEstateCommand
            {
                Name = "Apartamento",
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTypeOfRealEstateCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<Unit>(Unit.Value));

            //Act
            var result = await _typeOfRealEstateController.Post(createTypeOfRealEstateCommand);

            //Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }
        [Fact]
        public async Task CreateTypeOfRealEstate_ShouldReturnBadRequestForNameEmpty()
        {
            // Arrange
            var CreateTypeOfRealEstateCommand = new CreateTypeOfRealEstateCommand
            {
                Name = "",
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTypeOfRealEstateCommand>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new ApiException("No pueden haber campos nulos", 400));

            // Act
            var ex = await Assert.ThrowsAsync<ApiException>(() => _typeOfRealEstateController.Post(CreateTypeOfRealEstateCommand));

            // Assert
            Assert.Equal(400, ex.ErrorCode);
        }
        [Fact]
        public async Task GetById_ShouldReturnOkResultWithTypeOfRealEstate()
        {
            // Arrange
            var typeOfRealEstate = new Response<TypeOfRealEstateDto> { Succeeded = true, Message = "", Errors = null, Data = new TypeOfRealEstateDto { Id = 1,Name = "Apartamento" } };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTypeOfRealEstateByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(typeOfRealEstate);

            // Act
            var result = await _typeOfRealEstateController.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(typeOfRealEstate, okResult.Value);
        }
        

        [Fact]
        public async Task GetAll_ShouldReturnOkResultWithTypeOfRealEstateList()
        {
            // Arrange
            var typeOfRealEstate = new Response<IEnumerable<TypeOfRealEstateDto>>
            {
                Succeeded = true,
                Message = "",
                Errors = null,
                Data = new List<TypeOfRealEstateDto>
            {
                new TypeOfRealEstateDto
                {
                    Id = 1,
                    Name = "Apartamento"
                },
                new TypeOfRealEstateDto
                {
                    Id = 2,
                    Name = "Casa"
                },
                new TypeOfRealEstateDto
                {
                    Id = 3,
                    Name = "PentHouse"
                }
            }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTypeOfRealEstateQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(typeOfRealEstate);

            // Act
            var result = await _typeOfRealEstateController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(typeOfRealEstate, okResult.Value);
        }

        [Fact]
        public async Task Delete_ShouldReturn204StatusCode_WhenValidRequest()
        {
            // Arrange
            int id = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTypeOfRealEstatetCommand>(), default))
                         .ReturnsAsync(new Response<int>(1));

            // Act
            var result = await _typeOfRealEstateController.Delete(id);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }
    }
}

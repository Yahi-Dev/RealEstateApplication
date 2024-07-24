using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstateApplication.Application.Dtos.API.Improvement;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Features.Improvements.Commands;
using RealEstateApplication.Application.Features.Improvements.Queries;
using RealEstateApplication.Application.Wrappers;
using RealEstateApplication.WebApi.Controllers.V1;

namespace RealEstateApplication.Test.Improvements
{
    public class ImprovementTestController
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ImprovementController _improvementController;

        public ImprovementTestController()
        {
            _mediatorMock = new Mock<IMediator>();
            _improvementController = new ImprovementController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateImprovement_ShouldReturnStatusCode201()
        {
            //Arrange
            var createImprovementCommand = new CreateImprovementCommand
            {
                Name = "Balcon",
                Description = "Nuevo Balcon"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateImprovementCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response<int>(1));

            //Act
            var result = await _improvementController.Post(createImprovementCommand);

            //Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }
        [Fact]
        public async Task CreateImprovement_ShouldReturnBadRequestForNameEmpty()
        {
            // Arrange
            var createImprovementCommand = new CreateImprovementCommand
            {
                Name = "",
                Description = "Nuevo Balcon"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateImprovementCommand>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new ApiException("No pueden haber campos nulos", 400));

            // Act
            var ex = await Assert.ThrowsAsync<ApiException>(() => _improvementController.Post(createImprovementCommand));

            // Assert
            Assert.Equal(400, ex.ErrorCode);
            Assert.Equal("No pueden haber campos nulos", ex.Message);
        }
        [Fact]
        public async Task CreateImprovement_ShouldReturnBadRequestForDescriptionEmpty()
        {
            // Arrange
            var createImprovementCommand = new CreateImprovementCommand
            {
                Name = "Balcon",
                Description = ""
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateImprovementCommand>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new ApiException("No pueden haber campos nulos", 400));

            // Act
            var ex = await Assert.ThrowsAsync<ApiException>(() => _improvementController.Post(createImprovementCommand));

            // Assert
            Assert.Equal(400, ex.ErrorCode);
            Assert.Equal("No pueden haber campos nulos", ex.Message);
        }
        [Fact]
        public async Task GetById_ShouldReturnOkResultWithImprovement()
        {
            // Arrange
            var improvement = new Response<ImprovementDto> { Succeeded = true, Message = "", Errors = null, Data = new ImprovementDto { Id = 1, Description = "Balcon", Name = "Balcon 1" } };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetImprovementByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(improvement);

            // Act
            var result = await _improvementController.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(improvement, okResult.Value);
        }
        [Fact]
        public async Task GetAll_ShouldReturnOkResultWithImprovementList()
        {
            // Arrange
            var improvement = new Response<IEnumerable<ImprovementDto>>
            {
                Succeeded = true,
                Message = "",
                Errors = null,
                Data = new List<ImprovementDto>
            {
                new ImprovementDto
                {
                    Id = 1,
                    Description = "Balcon 1",
                    Name = "Balcon 1"
                },
                new ImprovementDto
                {
                    Id = 2,
                    Description = "Balcon 2",
                    Name = "Balcon 2"
                },
                new ImprovementDto
                {
                    Id = 3,
                    Description = "Balcon 3",
                    Name = "Balcon 3"
                },
                new ImprovementDto
                {
                    Id = 4,
                    Description = "Balcon 4",
                    Name = "Balcon 4"
                }
            }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllImprovementsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(improvement);

            // Act
            var result = await _improvementController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(improvement, okResult.Value);
        }

    }
}

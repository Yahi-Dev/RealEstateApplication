using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstateApplication.Application.Features.Improvements.Commands;
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
    }
}

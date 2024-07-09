
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstateApp.Presentation.WebAPI.Controllers.V1;
using RealEstateApplication.Application.Dtos.API.RealState;
using RealEstateApplication.Application.Features.Improvements.Queries;
using RealEstateApplication.Application.Features.RealState.Queries.GetAllRealState;
using RealEstateApplication.Application.Features.RealState.Queries.GetRealStateByCode;
using RealEstateApplication.Application.Features.RealState.Queries.GetRealStateById;
using RealEstateApplication.Application.Wrappers;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Test
{
    public class RealEstateTestController
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly RealEstateController _realEstateController;

        public RealEstateTestController()
        {
            _mediatorMock = new Mock<IMediator>();
            _realEstateController = new RealEstateController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResultWithRealEstateList()
        {
            // Arrange
            var realEstates = new Response<IEnumerable<RealEstateDto>>
            {
                Succeeded = true,
                Message = "",
                Errors = null,
                Data = new List<RealEstateDto>
                {
                    new RealEstateDto
                    {
                        Id = 1,
                        IdAgent = "agent1",
                        Code = "RE123",
                        BathRooms = 2,
                        BedRooms = 3,
                        Size = 120,
                        Price = 250000,
                        Description = "Beautiful house",
                        Address = "123 Main St"
                    },
                    new RealEstateDto
                    {
                        Id = 2,
                        IdAgent = "agent2",
                        Code = "RE456",
                        BathRooms = 3,
                        BedRooms = 4,
                        Size = 200,
                        Price = 500000,
                        Description = "Luxurious villa",
                        Address = "456 Oak St"
                    }
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllRealEstateQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(realEstates);


            // Act
            var result = await _realEstateController.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(realEstates, okResult.Value);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResultWithRealEstate()
        {
            // Arrange
            var realEstate = new Response<RealEstateDto>
            {
                Succeeded = true,
                Message = "",
                Errors = null,
                Data = new RealEstateDto
                {
                    Id = 1,
                    IdAgent = "agent1",
                    Code = "RE123",
                    BathRooms = 2,
                    BedRooms = 3,
                    Size = 120,
                    Price = 250000,
                    Description = "Beautiful house",
                    Address = "123 Main St"
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRealEstateByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(realEstate);

            // Act
            var result = await _realEstateController.GetById(new GetRealEstateByIdQuery { Id = 1 });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(realEstate, okResult.Value);
        }

        [Fact]
        public async Task GetById_ShouldReturnNoContent_WhenRealEstateNotFound()
        {
            // Arrange
            var realEstate = new Response<RealEstateDto>
            {
                Succeeded = false,
                Message = "Real estate not found",
                Errors = null,
                Data = null
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRealEstateByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(realEstate);

            // Act
            var result = await _realEstateController.GetById(new GetRealEstateByIdQuery { Id = 999 });

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async Task GetByCode_ShouldReturnOkResultWithRealEstate()
        {
            // Arrange
            var realEstate = new Response<RealEstateDto>
            {
                Succeeded = true,
                Message = "",
                Errors = null,
                Data = new RealEstateDto
                {
                    Id = 1,
                    IdAgent = "agent1",
                    Code = "RE123",
                    BathRooms = 2,
                    BedRooms = 3,
                    Size = 120,
                    Price = 250000,
                    Description = "Beautiful house",
                    Address = "123 Main St"
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRealEstateByCodeQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(realEstate);

            // Act
            var result = await _realEstateController.GetByCode(new GetRealEstateByCodeQuery { Code = "RE123" });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(realEstate, okResult.Value);
        }

        [Fact]
        public async Task GetByCode_ShouldReturnNoContent_WhenRealEstateNotFound()
        {
            // Arrange
            var realEstate = new Response<RealEstateDto>
            {
                Succeeded = false,
                Message = "Real estate not found",
                Errors = null,
                Data = null
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRealEstateByCodeQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(realEstate);

            // Act
            var result = await _realEstateController.GetByCode(new GetRealEstateByCodeQuery { Code = "NONEXISTENT" });

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }
    }
}

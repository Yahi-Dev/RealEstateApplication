using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApplication.Application.Features.RealState.Queries.GetAllRealState;
using RealEstateApplication.Application.Features.RealState.Queries.GetRealStateByCode;
using RealEstateApplication.Application.Features.RealState.Queries.GetRealStateById;
using RealEstateApplication.Application.Interfaces.Services;
using RealEstateApplication.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Presentation.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class RealEstateController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RealEstateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("GetAll")]
        [SwaggerOperation(
            Summary = "Listado de propiedades",
            Description = "Obtiene el listado de todas las propiedades creadas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllRealEstateQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("GetById")]
        [SwaggerOperation(
            Summary = "Propiedad por id.",
            Description = "Obtiene una propiedad filtrada por id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(GetRealEstateByIdQuery request)
        {
            var ok = await _mediator.Send(new GetRealEstateByIdQuery { Id = request.Id });
            if (!ok.Succeeded)
            {
                return NoContent();
            }
            return Ok(ok);
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("GetByCode")]
        [SwaggerOperation(
            Summary = "Propiedad por codigo.",
            Description = "Obtiene una propiedad filtrada por codigo.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCode(GetRealEstateByCodeQuery request)
        {
            var ok = await _mediator.Send(new GetRealEstateByCodeQuery { Code = request.Code });
            if (!ok.Succeeded)
            {
                return NoContent();
            }
            return Ok(ok);
        }
    }
}

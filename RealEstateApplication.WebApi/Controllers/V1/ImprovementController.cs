using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApplication.Application.Features.Improvements.Commands;
using RealEstateApplication.Application.Features.Improvements.Queries;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApplication.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Mejoras")]
    public class ImprovementController : BaseApiController
    {

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Creacion de una mejora",
            Description = "Recibe los parametros que necesita para crear una mejora")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateImprovementCommand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateImprovementCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Debe enviar los datos correctamente");
            }
            var response = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, "Mejora creada correctamente");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [SwaggerOperation(
           Summary = "Listado de mejoras",
           Description = "Obtiene el listado de todas las mejoras creadas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllImprovementsQuery()));
        }
    }
}

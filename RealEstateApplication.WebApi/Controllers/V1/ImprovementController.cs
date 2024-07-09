using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApplication.Application.Features.Improvements.Commands;
using RealEstateApplication.Application.Features.Improvements.Queries;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApplication.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Mejoras")]
    public class ImprovementController(IMediator mediator) : BaseApiController
    {

        [Authorize(Roles = "Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creacion de una mejora",
            Description = "Recibe los parametros que necesita para crear una mejora")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateImprovementCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Debe enviar los datos correctamente");
            }
            var response = await mediator.Send(command);
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
            return Ok(await mediator.Send(new GetAllImprovementsQuery()));
        }
        [Authorize(Roles = "Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Mejora por id",
            Description = "Obtiene una mejora filtrada por su id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await mediator.Send(new GetImprovementByIdQuery { Id = id }));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualizacion de una mejora",
            Description = "Recibe los paramentros necesarios para modificar una mejora existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateImprovementCommand command, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Debe enviar los datos correctamente");
            }
            if (command.Id != id)
            {
                return BadRequest("Debe enviar los datos correctamente");
            }
            await mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Eliminar una mejorar",
            Description = "Recibe los parametros necesarios para eliminar una mejora existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteImprovementCommand { Id = id });
            return NoContent();
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Features.Improvements.Commands;
using RealEstateApplication.Application.Features.Improvements.Queries;
using RealEstateApplication.Application.Features.TypeOfRealEstates.Commands;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApplication.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Tipos de propiedades ")]
    public class TypeOfRealEstateController(IMediator mediator) : BaseApiController
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Creacion de un tipo de propiedades",
            Description = "Recibe los parametros que necesita para crear un tipo de propiedad")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateTypeOfRealEstateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Debe enviar los datos correctamente");
            }
            var response = await mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, "Tipo de propiedad creada correctamente");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [SwaggerOperation(
           Summary = "Listado de tipos de propiedades",
           Description = "Obtiene el listado de todos los tipos de propiedades creados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetAllTypeOfRealEstateQuery()));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Mejora por id",
            Description = "Obtiene una mejora filtrada por su id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await mediator.Send(new GetTypeOfRealEstateByIdQuery { Id = id }));
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
        public async Task<IActionResult> Put(UpdateTypeOfRealEstateCommand command)
        {
            if (!ModelState.IsValid)
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
            await mediator.Send(new DeleteTypeOfRealEstatetCommand { Id = id });
            return NoContent();
        }
    }
}

using AutoMapper;
using MediatR;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApplication.Application.Features.TypeOfRealEstates.Commands
{
    /// <summary>
    /// Parametros para la elimiacion de una mejora.
    /// </summary>
    public class DeleteTypeOfRealEstatetCommand : IRequest<Response<int>>
    {
        ///<example> 1</example>
        [SwaggerParameter(Description = "El id del tipo de propiedad que se quiere eliminar")]

        public int Id { get; set; }
    }
    public class DeleteTypeOfRealEstatetCommandHandler(ITypeOfRealEstateRepository typeOfRealEstate, IMapper mapper) : IRequestHandler<DeleteTypeOfRealEstatetCommand, Response<int>>
    {
        public async Task<Response<int>> Handle(DeleteTypeOfRealEstatetCommand command, CancellationToken cancellationToken)
        {
            var typeOfReal = await typeOfRealEstate.GetByIdAsync(command.Id);

            if (typeOfReal == null) throw new ApiException("TypeOfRealEstate not found", (int)HttpStatusCode.NoContent);

            await typeOfRealEstate.DeleteAsync(typeOfReal);
            return new Response<int>(1);
        }
    }
}

using AutoMapper;
using MediatR;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Application.Wrappers;
using RealEstateApplication.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApplication.Application.Features.TypeOfRealEstates.Commands
{
    /// <summary>
    /// Parametros para la actualizacion de una mejora.
    /// </summary>
    public class UpdateTypeOfRealEstateCommand : IRequest<Response<Unit>>
    {
        [SwaggerParameter(Description = "Id de la mejora")]
        public int Id { get; set; }

        [SwaggerParameter(Description = "El nuevo nombre de la mejora")]
        public string Name { get; set; } = null!;
    }

    public class UpdateTypeOfRealEstateCommandHandler(ITypeOfRealEstateRepository realEstateRepository, IMapper mapper) : IRequestHandler<UpdateTypeOfRealEstateCommand, Response<Unit>>
    {


        public async Task<Response<Unit>> Handle(UpdateTypeOfRealEstateCommand command, CancellationToken cancellationToken)
        {
            var typeOfRealEstate = await realEstateRepository.GetByIdAsync(command.Id);

            if (typeOfRealEstate is null) throw new ApiException("Improvement not found", (int)HttpStatusCode.NoContent);

            typeOfRealEstate = mapper.Map<TypeOfRealEstate>(command);

            await realEstateRepository.UpdateAsync(typeOfRealEstate, typeOfRealEstate.Id);


            return new Response<Unit>(Unit.Value);
        }
    }
}

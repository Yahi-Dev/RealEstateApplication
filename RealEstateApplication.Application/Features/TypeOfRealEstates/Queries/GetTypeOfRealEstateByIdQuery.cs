using AutoMapper;
using MediatR;
using RealEstateApplication.Application.Dtos.TypeOfRealEstate;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApplication.Application.Features.TypeOfRealEstates.Commands
{
    /// <summary>
    /// Parametros para obtener una mejora por Id.
    /// </summary>
    public class GetTypeOfRealEstateByIdQuery : IRequest<Response<TypeOfRealEstateDto>>
    {
        /// <example>
        /// 1
        /// </example>
        [SwaggerParameter(Description = "Debe colocar el id del tipo de propiedad que quiere obtener")]

        public int Id { get; set; }
    }

    public class GetTypeOfRealEstateByIdQueryHandle(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper) : IRequestHandler<GetTypeOfRealEstateByIdQuery, Response<TypeOfRealEstateDto>>
    {

        public async Task<Response<TypeOfRealEstateDto>> Handle(GetTypeOfRealEstateByIdQuery request, CancellationToken cancellationToken)
        {
            TypeOfRealEstateDto typeOfRealEstate = mapper.Map<TypeOfRealEstateDto>(await typeOfRealEstateRepository.GetByIdAsync(request.Id));
            if (typeOfRealEstate is null) throw new ApiException("Tipo de propiedad no encontrada", (int)HttpStatusCode.NoContent);
            return new Response<TypeOfRealEstateDto>(typeOfRealEstate);
        }

    }
}

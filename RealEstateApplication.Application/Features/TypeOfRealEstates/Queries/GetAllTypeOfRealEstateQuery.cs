using AutoMapper;
using MediatR;
using RealEstateApplication.Application.Dtos.TypeOfRealEstate;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Application.Wrappers;
using System.Net;

namespace RealEstateApplication.Application.Features.TypeOfRealEstates.Commands
{
    /// <summary>
    /// Listar todas las mejoras
    /// </summary>
    public class GetAllTypeOfRealEstateQuery : IRequest<Response<IEnumerable<TypeOfRealEstateDto>>>
    {
    }

    public class GetAllTypeOfRealEstateQueryHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper) : IRequestHandler<GetAllTypeOfRealEstateQuery, Response<IEnumerable<TypeOfRealEstateDto>>>
    {
        public async Task<Response<IEnumerable<TypeOfRealEstateDto>>> Handle(GetAllTypeOfRealEstateQuery request, CancellationToken cancellationToken)
        {

            List<TypeOfRealEstateDto> typeOfRealEstate = mapper.Map<List<TypeOfRealEstateDto>>(await typeOfRealEstateRepository.GetAllAsync());

            if (typeOfRealEstate.Count == 0) throw new ApiException("No se encontraron tipos de propiedades", (int)HttpStatusCode.NoContent);

            return new Response<IEnumerable<TypeOfRealEstateDto>>(typeOfRealEstate);
        }
    }
}

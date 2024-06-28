using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Application.Wrappers;
using System.Net;

namespace RealEstateApplication.Application.Features.Improvements.Queries
{
    /// <summary>
    /// Listar todas las mejoras
    /// </summary>
    public class GetAllImprovementsQuery : IRequest<Response<IEnumerable<ImprovementDto>>>
    {
    }

    public class GetAllImprovementsQueryHandler : IRequestHandler<GetAllImprovementsQuery, Response<IEnumerable<ImprovementDto>>>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public GetAllImprovementsQueryHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ImprovementDto>>> Handle(GetAllImprovementsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<ImprovementDto> improvementsDto = _mapper.Map<List<ImprovementDto>>(await _improvementRepository.GetAllAsync());

                if (improvementsDto.Count == 0) throw new ApiException("No se encontraron mejoras", (int)HttpStatusCode.NoContent);

                return new Response<IEnumerable<ImprovementDto>>(improvementsDto);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, (int)HttpStatusCode.InternalServerError);
            }

        }
    }
}

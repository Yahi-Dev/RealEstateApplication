using AutoMapper;
using MediatR;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Application.Wrappers;
using RealEstateApplication.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApplication.Application.Features.Improvements.Commands
{
    public class CreateImprovementCommand : IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El nombre de la mejora")]
        public string Name { get; set; } = null!;
        [SwaggerParameter(Description = "Descripcion de la mejora")]
        public string Description { get; set; } = null!;
    }
    public class CreateImprovementCommandHandler : IRequestHandler<CreateImprovementCommand, Response<int>>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public CreateImprovementCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateImprovementCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(command.Name))
                {
                    throw new ApiException("No pueden haber campos nulos", 400);
                }
                Improvement imrproment = _mapper.Map<Improvement>(command);
                imrproment = await _improvementRepository.AddAsync(imrproment);
                return new Response<int>(imrproment.Id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }


        }
    }
}

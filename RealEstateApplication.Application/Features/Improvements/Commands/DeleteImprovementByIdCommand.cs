﻿using AutoMapper;
using MediatR;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApplication.Application.Features.Improvements.Commands
{
    /// <summary>
    /// Parametros para la elimiacion de una mejora.
    /// </summary>
    public class DeleteImprovementCommand : IRequest<Response<int>>
    {
        ///<example> 1</example>
        [SwaggerParameter(Description = "El id de la mejora que se quiere eliminar")]

        public int Id { get; set; }
    }
    public class DeleteImprovementByIdCommandHandler : IRequestHandler<DeleteImprovementCommand, Response<int>>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public DeleteImprovementByIdCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeleteImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _improvementRepository.GetByIdAsync(command.Id);

            if (improvement == null) throw new ApiException("Improvement not found",(int)HttpStatusCode.NoContent);

            await _improvementRepository.DeleteAsync(improvement);
            return new Response<int>(improvement.Id);
        }
    }
}

﻿using AutoMapper;
using MediatR;
using RealEstateApplication.Application.Dtos.API.RealState;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Application.Interfaces.Services;
using RealEstateApplication.Application.ViewModel.RealEstate;
using RealEstateApplication.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApplication.Application.Features.RealState.Queries.GetRealStateByCode
{
    public class GetRealEstateByCodeQuery : IRequest<Response<RealEstateDto>>
    {
        [SwaggerParameter(Description = "Debe colocar el codigo de la propiedad que quiere obtener")]
        public string Code { get; set; }
    }

    public class GetRealEstateByCodeQueryHandler : IRequestHandler<GetRealEstateByCodeQuery, Response<RealEstateDto>>
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IAccountService _accountService;

        public GetRealEstateByCodeQueryHandler(IRealEstateRepository realEstateRepository, IAccountService accountService)
        {
            _realEstateRepository = realEstateRepository;
            _accountService = accountService;
        }

        public async Task<Response<RealEstateDto>> Handle(GetRealEstateByCodeQuery request, CancellationToken cancellationToken)
        {
            var realStateDto = await GetRealEstateByCodeAsync(request.Code);

            if (realStateDto is null) throw new ApiException($"RealState not found", (int)HttpStatusCode.NoContent);

            return new Response<RealEstateDto>(realStateDto);
        }
        private async Task<RealEstateDto> GetRealEstateByCodeAsync(string code)
        {
            var realEstateList = new List<RealEstateDto>();
            var realEstates = await _realEstateRepository.GetAllWithIncludeAsync(new List<string> { "TypeOfSale", "TypeOfRealEstate", "RealEstateImprovements.Improvement" });
            var realEstateView = realEstates.FirstOrDefault(x => x.Code == code);

            if(realEstateView is null)
            {
                return null;
            }
            var user = await _accountService.GetUserByIdAsync(realEstateView.IdAgent);
            var realEstateDto = new RealEstateDto()
            {
                Id = realEstateView.Id,
                Description = realEstateView.Description,
                BathRooms = realEstateView.BathRooms,
                BedRooms = realEstateView.BedRooms,
                Size = realEstateView.Size,
                Code = realEstateView.Code,
                IdAgent = realEstateView.IdAgent,
                AgentEmail = user.Email,
                AgentName = user.FirstName + " " + user.LastName,
                Price = realEstateView.Price,
                Address = realEstateView.Address,
                TypeOfRealEstateName = realEstateView.TypeOfRealEstate.Name,
                TypeOfSaleName = realEstateView.TypeOfSale.Name,
                ImprovementName = realEstateView.RealEstateImprovements.Select(x => x.Improvement.Name).ToList(),
            };

            return realEstateDto;
        }

    }
}

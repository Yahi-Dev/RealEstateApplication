using AutoMapper;
using RealEstateApplication.Core.Application.Dtos.API.Improvement;
using RealEstateApplication.Application.Features.Improvements.Commands;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Application.Mapping
{
    public class ImprovementProfile : Profile
    {
        public ImprovementProfile()
        {
            CreateMap<Improvement, CreateImprovementCommand>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Improvement, SaveImprovementDto>()
              .ReverseMap()
              .ForMember(x => x.LastModified, opt => opt.Ignore())
              .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
              .ForMember(x => x.Created, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Improvement, ImprovementDto>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Improvement, UpdateImprovementCommand>()
               .ReverseMap()
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore());
        }
    }
}

using AutoMapper;
using RealEstateApplication.Application.Dtos.TypeOfRealEstate;
using RealEstateApplication.Application.Features.TypeOfRealEstates.Commands;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Application.Mapping
{
    public class TypeOfRealEstateProfile : Profile
    {
        public TypeOfRealEstateProfile()
        {

            CreateMap<TypeOfRealEstate, TypeOfRealEstateDto>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfRealEstate, CreateTypeOfRealEstateCommand>()
              .ReverseMap()
              .ForMember(x => x.LastModified, opt => opt.Ignore())
              .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
              .ForMember(x => x.Created, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfRealEstate, UpdateTypeOfRealEstateCommand>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());
        }
    }
}

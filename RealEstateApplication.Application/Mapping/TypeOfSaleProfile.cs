﻿using AutoMapper;
using RealEstateApplication.Application.Dtos.API.TypeOfSale;
using RealEstateApplication.Application.Features.TypeOfSales.Command.CreateTypeOfSale;
using RealEstateApplication.Application.Features.TypeOfSales.Command.UpdateTypeOfSale;
using RealEstateApplication.Application.ViewModel.TypeOfSale;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Application.Mappings
{
    public class TypeOfSaleProfile:Profile
    {
        public TypeOfSaleProfile()
        {
            CreateMap<TypeOfSale, SaveTypeOfSaleDto>()
               .ReverseMap()
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfSale, TypeOfSaleDto>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());  
            
            CreateMap<TypeOfSale, CreateTypeOfSaleCommand>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());  
            
            CreateMap<TypeOfSale, UpdateTypeOfSaleCommand>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())     
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfSale, TypeOfSaleViewModel>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfSale, SaveTypeOfSaleViewModel>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<TypeOfSaleDto, TypeOfSaleViewModel>()
                .ReverseMap();

            CreateMap<TypeOfSaleDto, SaveTypeOfSaleViewModel>()
                .ReverseMap();
        }
    }
}

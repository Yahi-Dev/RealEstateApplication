using RealEstateApplication.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApplication.Core.Application.ViewModel.TypeOfSale;
using RealEstateApplication.Core.Domain.Entities;

namespace RealEstateApplication.Core.Application.Interfaces.Services
{
    public interface ITypeOfSaleService:IGenericService<TypeOfSale, SaveTypeOfSaleViewModel, TypeOfSaleViewModel>
    {
    }
}

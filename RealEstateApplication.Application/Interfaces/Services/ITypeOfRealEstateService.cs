using RealEstateApplication.Application.ViewModel.TypeOfRealState;
using RealEstateApplication.Application.Interfaces.Services;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Application.Interfaces.Services
{
    public interface ITypeOfRealEstateService:IGenericService<TypeOfRealEstate,SaveTypeOfRealStateViewModel,TypeOfRealStateViewModel>
    {
    }
}

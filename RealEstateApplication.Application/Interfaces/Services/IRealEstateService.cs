using RealEstateApplication.Application.ViewModel.RealEstate;
using RealEstateApplication.Core.Application.Interfaces.Services;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Application.Interfaces.Services
{
    public interface IRealEstateService : IGenericService<RealEstate, SaveRealEstateViewModel, RealEstateViewModel>
    {
        Task<RealEstateViewModel> GetRealEstateViewModelById(int id);
        int CountRealState();
        Task DeleteByAgent(string  IdAgent);
        Task<List<RealEstateViewModel>> GetAllByAgent(string idAgent);
        Task<List<RealEstateViewModel>> GetAllWithFilters(string name, int toilets, int bedrooms, decimal minPrice, decimal maxPrice,string code);
        Task<List<RealEstateViewModel>> GetFavoritesByClient();
        Task<SaveRealEstateViewModel> UpdateRealEstate(SaveRealEstateViewModel model, int id);
        Task<List<int>> GetRealEstateByTypeAsync(int IdTypeRealEstate);
        Task<List<int>> GetRealEstateByTypeOfSaleAsync(int IdTypeOfSale);
    }
}
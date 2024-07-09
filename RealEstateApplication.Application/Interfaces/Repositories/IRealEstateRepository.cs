using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Application.Interfaces.Repositories
{
    public interface IRealEstateRepository : IBaseRepository<RealEstate>
    {
        Task<List<int>> GetRealEstateByTypeAsync(int IdTypeRealEstate); 
        Task<List<int>> GetRealEstateByTypeOfSaleAsync(int IdTypeOfSale);
        Task DeleteByAgent(string IdAgent);
    }
}

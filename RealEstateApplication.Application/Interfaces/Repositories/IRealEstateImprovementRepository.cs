using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Application.Interfaces.Repositories
{
    public interface IRealEstateImprovementRepository : IBaseRepository<RealEstateImprovements>
    {
        Task RemoveAll(int idRealEstate);
        IEnumerable<RealEstateImprovements> GetImprovementId(int id);
        Task RemoveOne(int idImprovement, int idRealEstate);
    }
}

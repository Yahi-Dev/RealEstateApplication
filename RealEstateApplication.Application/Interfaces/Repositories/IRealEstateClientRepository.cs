using RealEstateApplication.Domain.Entities;
using RealEstateApplication.Application.Interfaces.Repositories;

namespace RealEstateApplication.Application.Interfaces.Repositories
{
    public interface IRealEstateClientRepository : IBaseRepository<RealEstateClient>
    {
        Task<List<RealEstateClient>> GetAllByIdUser(string idUser);
        Task<RealEstateClient> GetByIdsAsync(int idRealEstate, string idUser);
        Task RemoveAllByIdRealEstate(int idRealEstate);
    }
}

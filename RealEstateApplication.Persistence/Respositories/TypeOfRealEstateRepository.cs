using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Domain.Entities;
using RealEstateApplication.Persistence.Context;

namespace RealEstateApplication.Persistence.Respositories
{
    public class TypeOfRealEstateRepository : BaseRepository<TypeOfRealEstate>, ITypeOfRealEstateRepository
    {
        public TypeOfRealEstateRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}

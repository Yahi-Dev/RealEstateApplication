using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Domain.Entities;
using RealEstateApplication.Persistence.Context;
using RealEstateApplication.Persistence.Respositories;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class TypeOfRealEstateRepositoy : BaseRepository<TypeOfRealEstate>, ITypeOfRealEstateRepository
    {
        public TypeOfRealEstateRepositoy(ApplicationContext dbContext) : base(dbContext)
        {
        }

       
    }
}

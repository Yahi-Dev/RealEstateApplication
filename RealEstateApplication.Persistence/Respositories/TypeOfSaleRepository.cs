using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Domain.Entities;
using RealEstateApplication.Persistence.Context;
using RealEstateApplication.Persistence.Respositories;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class TypeOfSaleRepository : BaseRepository<TypeOfSale>,ITypeOfSaleRepository
    {
        public TypeOfSaleRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}

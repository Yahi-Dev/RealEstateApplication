using RealEstateApp.Infraestructure.Persistence.Repositories;
using RealEstateApplication.Core.Application.Interfaces.Repositories;
using RealEstateApplication.Core.Domain.Entities;
using RealEstateApplication.Persistence.Context;

namespace RealEstateApplication.Infraestructure.Persistence.Repositories
{
    public class TypeOfSaleRepository : GenericRepository<TypeOfSale>,ITypeOfSaleRepository
    {
        public TypeOfSaleRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}

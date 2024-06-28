using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Domain.Entities;
using RealEstateApplication.Persistence.Context;

namespace RealEstateApplication.Persistence.Respositories
{
    public class ImprovementRepository : BaseRepository<Improvement>, IImprovementRepository
    {
        public ImprovementRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}

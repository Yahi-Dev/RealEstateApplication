﻿using Microsoft.EntityFrameworkCore;
using RealEstateApplication.Domain.Entities;
using RealEstateApplication.Persistence.Respositories;
using RealEstateApplication.Persistence.Context;
using RealEstateApplication.Application.Interfaces.Repositories;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class RealEstateImprovemnetRepository : BaseRepository<RealEstateImprovements>, IRealEstateImprovementRepository
    {
        private readonly ApplicationContext _dbContext;
        public RealEstateImprovemnetRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async  Task RemoveAll(int idRealEstate)
        {
            var realEstateImprovements = await Entities.Where(x => x.IdRealEstate == idRealEstate).ToListAsync();

            foreach (var realEstateImprovement in realEstateImprovements)
            {
                Entities.Remove(realEstateImprovement);
                await _dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<RealEstateImprovements> GetImprovementId(int id) => Entities.Where(x => x.IdImprovement == id);

        public async Task RemoveOne(int idImprovement, int idRealEstate)
        {
            var realEstateImprovements = Entities.Where(x => x.IdImprovement == idImprovement && x.IdRealEstate == idRealEstate).FirstOrDefault();

            Entities.Remove(realEstateImprovements);
            _dbContext.SaveChangesAsync();
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Domain.Entities;
using RealEstateApplication.Persistence.Context;
using RealEstateApplication.Persistence.Respositories;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class RealEstateRepository : BaseRepository<RealEstate>, IRealEstateRepository
    {
        private readonly ApplicationContext _dbContext;
        public RealEstateRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteByAgent(string IdAgent)
        {
            var realEstate = await Entities.Where(x => x.IdAgent == IdAgent).ToListAsync();
            foreach (var item in realEstate)
            {
                Entities.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<int>> GetRealEstateByTypeAsync(int IdTypeRealEstate)
        {
            List<int> idRealEstates = new();
            var realEstates = await Entities.Where(x => x.IdTypeOfRealEstate == IdTypeRealEstate).ToListAsync();
            foreach (var realEstate in realEstates)
            {
                idRealEstates.Add(realEstate.Id);
            }
            return idRealEstates;
        }

        public async Task<List<int>> GetRealEstateByTypeOfSaleAsync(int IdTypeOfSale)
        {
            List<int> idRealEstates = new();
            var realEstates = await Entities.Where(x => x.IdTypeOfSale == IdTypeOfSale).ToListAsync();
            foreach (var realEstate in realEstates)
            {
                idRealEstates.Add(realEstate.Id);
            }
            return idRealEstates;
        }
    }
}

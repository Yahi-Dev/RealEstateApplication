﻿
namespace RealEstateApplication.Interfaces.Repositories
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, int id);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
        Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties);
        int GetCount();
    }
}

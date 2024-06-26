namespace RealEstateApplication.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T Entity);
        Task UpdateAsync(T Entity, int id);
        Task DeleteAsync(T Entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllWithIncludeAsync(List<string> properties);
    }
}

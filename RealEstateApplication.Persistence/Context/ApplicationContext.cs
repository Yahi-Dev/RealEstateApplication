using Microsoft.EntityFrameworkCore;

namespace RealEstateApplication.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }




    }
}
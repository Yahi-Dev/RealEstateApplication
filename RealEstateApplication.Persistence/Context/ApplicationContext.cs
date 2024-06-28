using Microsoft.EntityFrameworkCore;
using RealEstateApplication.Core.Domain.Entities;
using RealEstateApplication.Infraestructure.Persistence.EntityConfigurations;
using RealEstateApplication.Domain.Common;
using RealEstateApplication.Domain.Entities;
using RealEstateApplication.Persistence.EntityConfiguration;

namespace RealEstateApplication.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ImprovementConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfSaleConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Improvement> Improvements { get; set; }
        public DbSet<TypeOfSale> TypeOfSales { get; set; }

    }
}
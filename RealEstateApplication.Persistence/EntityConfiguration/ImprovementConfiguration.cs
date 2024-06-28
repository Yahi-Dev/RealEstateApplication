using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Persistence.EntityConfiguration
{
    public class ImprovementConfiguration : IEntityTypeConfiguration<Improvement>
    {
        public void Configure(EntityTypeBuilder<Improvement> builder)
        {
            builder.ToTable("Improvement");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(60);
        }
    }
}

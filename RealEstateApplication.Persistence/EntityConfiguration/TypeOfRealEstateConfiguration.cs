using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Persistence.EntityConfiguration
{
    public class TypeOfRealEstateConfiguration : IEntityTypeConfiguration<TypeOfRealEstate>
    {
        public void Configure(EntityTypeBuilder<TypeOfRealEstate> builder)
        {
            builder.ToTable("TypeOfRealEstate");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Name).HasMaxLength(60);
            builder.HasIndex(x => x.Name).IsUnique();
        }

    }
}

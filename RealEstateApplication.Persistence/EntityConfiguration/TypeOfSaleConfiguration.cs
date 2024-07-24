using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Infraestructure.Persistence.EntityConfigurations
{
    public class TypeOfSaleConfiguration : IEntityTypeConfiguration<TypeOfSale>
    {
        public void Configure(EntityTypeBuilder<TypeOfSale> builder)
        {
            builder.ToTable("TypeOfSale");
            builder.HasKey(x => x.Id);

        }
    }
}

using RealEstateApplication.Domain.Common;

namespace RealEstateApplication.Core.Domain.Entities
{
    public class TypeOfSale:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}

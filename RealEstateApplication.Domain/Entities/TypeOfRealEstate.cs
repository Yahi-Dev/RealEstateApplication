using RealEstateApplication.Domain.Common;

namespace RealEstateApplication.Domain.Entities
{
    public class TypeOfRealEstate:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<RealEstate> RealEstates { get; set; } = null!;
    }
}

using RealEstateApplication.Domain.Common;
using RealEstateApplication.Domain.Entities;

namespace RealEstateApplication.Domain.Entities
{
    public class RealEstateImprovements:BaseEntity
    {
        public int IdRealEstate { get; set; }
        public int IdImprovement { get; set; }
        public RealEstate RealEstate { get; set; } = null!;
        public Improvement Improvement { get; set; } = null!;
    }
}

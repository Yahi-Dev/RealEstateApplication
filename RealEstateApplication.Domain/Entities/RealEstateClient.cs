using RealEstateApplication.Domain.Common;

namespace RealEstateApplication.Domain.Entities
{
    public class RealEstateClient: BaseEntity
    {
        public string IdCliente { get; set; }
        public int IdRealEstate { get; set; }
    }
}

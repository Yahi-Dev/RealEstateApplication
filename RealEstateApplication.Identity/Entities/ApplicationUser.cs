using Microsoft.AspNetCore.Identity;

namespace RealEstateApplication.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityCard { get; set; }
        public string ImageUser { get; set; }
        public int CountOfRealEstate { get; set; }
        public bool IsActive { get; set; }
    }
}

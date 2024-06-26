
namespace RealEstateApplication.Application.Dtos.User
{
    public class RegisterResponse
    {
        public string? IdUser { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}

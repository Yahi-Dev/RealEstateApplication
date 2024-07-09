using RealEstateApplication.Application.Dtos.User;

namespace RealEstateApplication.Application.Interfaces.Services
{
    public interface IAccountService
    {
        #region Register and Authentication
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task SignOutAsync();

        #endregion

        #region Gets
        Task<List<AuthenticationResponse>> GetAllAsync(string entity);
        Task<AuthenticationResponse> GetUserByIdAsync(string id);
        #endregion
    }
}

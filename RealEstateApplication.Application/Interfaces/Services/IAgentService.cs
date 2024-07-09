using RealEstateApplication.Application.Dtos.Accounts;
using RealEstateApplication.Application.ViewModel.User;

namespace RealEstateApplication.Application.Interfaces.Services
{
    public interface IAgentService
    {
        Task<List<UserViewModel>> GetAllAgentAsync();
        Task<UserViewModel> GetAgentByIdAsync(string id);
        Task<AuthenticationResponse> ChangeStatus(string id,bool status);
        Task<List<UserViewModel>> GetAllWithFilterAsync(string name);
        Task IncrementRealEstate(string idAgent,int count);
    }
}

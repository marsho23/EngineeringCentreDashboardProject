using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Models.Request;

namespace EngineeringCentreDashboard.Interfaces
{
    public interface IUserLoginHelper
    {
        Task<UserLoginRequest> Get(int id);
        Task<UserLoginRequest> Add(UserLoginRequest userLogin);
        Task<IEnumerable<UserLoginRequest>> GetAll();
        Task<UserLoginRequest> Update(UserLoginRequest userLogin);
        Task Delete(int id);
        Task<UserLoginRequest> GetOrCreateUser(string email);

    }
}

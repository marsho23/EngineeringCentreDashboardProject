using EngineeringCentreDashboard.Models;

namespace EngineeringCentreDashboard.Interfaces
{
    public interface IUserLoginHelper
    {
        Task<UserLogin> Get(int id);
        Task<UserLogin> Add(UserLogin userLogin);
        Task<IEnumerable<UserLogin>> GetAll();
        Task<UserLogin> Update(UserLogin userLogin);
        Task Delete(int id);
    }
}

using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Models.Request;

namespace EngineeringCentreDashboard.Interfaces
{
    public interface IToDoHelper
    {
        Task<ToDoRequest> Get(int id);
        Task<ToDoRequest> Add(ToDoRequest toDo);
        Task<IEnumerable<ToDoRequest>> GetAll();
        Task<IEnumerable<ToDoRequest>> GetByUserLoginId(string email);

        Task<ToDoRequest> Update(ToDoRequest toDo);
        Task Delete(long id);
        Task<ToDoRequest> CompleteTask(long id);
        Task DeleteAllCompletedAsync(int userLoginId);
    }
}

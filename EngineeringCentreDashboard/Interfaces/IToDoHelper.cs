using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Models.Request;

namespace EngineeringCentreDashboard.Interfaces
{
    public interface IToDoHelper
    {
        Task<ToDoRequest> Get(int id);
        Task<ToDoRequest> Add(ToDoRequest toDo);
        Task<IEnumerable<ToDoRequest>> GetAll();
        Task<ToDoRequest> Update(ToDoRequest toDo);
        Task Delete(int id);
    }
}

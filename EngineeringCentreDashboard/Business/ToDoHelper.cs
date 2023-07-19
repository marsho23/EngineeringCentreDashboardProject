//using EngineeringCentreDashboard.Data;
//using EngineeringCentreDashboard.Interfaces;
//using EngineeringCentreDashboard.Models;
//using EngineeringCentreDashboard.Models.Request;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace EngineeringCentreDashboard.Business
//{
//    public class ToDoHelper : IToDoHelper
//    {
//        private readonly EngineeringDashboardDbContext _engineeringDashboardDbContext;

//        public ToDoHelper(EngineeringDashboardDbContext engineeringDashboardDbContext)
//        {
//            _engineeringDashboardDbContext = engineeringDashboardDbContext;
//        }

//        public async Task<ToDoRequest> Get(int id)
//        {
//            return await _engineeringDashboardDbContext.ToDoItems.FindAsync(id);
//        }

//        public async Task<ToDoRequest> Add(ToDoRequest toDo)
//        {
//            ToDo toDoDbModel = new ToDo(toDo);
//            await _engineeringDashboardDbContext.ToDoItems.AddAsync(toDoDbModel);
//            await _engineeringDashboardDbContext.SaveChangesAsync();
//            return toDo;
//        }

//        public async Task<IEnumerable<ToDoRequest>> GetAll()
//        {
//            return await _engineeringDashboardDbContext.ToDoItems.ToListAsync();
//        }

//        public async Task<ToDoRequest> Update(ToDoRequest toDo)
//        {
//            _engineeringDashboardDbContext.ToDoItems.Update(toDo);
//            await _engineeringDashboardDbContext.SaveChangesAsync();
//            return toDo; 
//        }

//        public async Task Delete(int id)
//        {
//            var toDo = await _engineeringDashboardDbContext.ToDoItems.FindAsync(id);
//            if (toDo != null)
//            {
//                _engineeringDashboardDbContext.ToDoItems.Remove(toDo);
//                await _engineeringDashboardDbContext.SaveChangesAsync();
//            }
//        }
//    }
//}

using EngineeringCentreDashboard.Data;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Models.Request;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineeringCentreDashboard.Business
{
    public class ToDoHelper : IToDoHelper
    {
        private readonly EngineeringDashboardDbContext _engineeringDashboardDbContext;

        public ToDoHelper(EngineeringDashboardDbContext engineeringDashboardDbContext)
        {
            _engineeringDashboardDbContext = engineeringDashboardDbContext;
        }

        public async Task<ToDoRequest> Get(int id)
        {
            ToDo toDo = await _engineeringDashboardDbContext.ToDoItems.FindAsync(id);
            return new ToDoRequest(toDo);
        }

        public async Task<ToDoRequest> Add(ToDoRequest toDo)
        {
            ToDo toDoDbModel = new ToDo(toDo);
            await _engineeringDashboardDbContext.ToDoItems.AddAsync(toDoDbModel);
            await _engineeringDashboardDbContext.SaveChangesAsync();
            return new ToDoRequest(toDoDbModel);
        }

        public async Task<IEnumerable<ToDoRequest>> GetAll()
        {
            List<ToDo> toDoItems = await _engineeringDashboardDbContext.ToDoItems.ToListAsync();
            return toDoItems.Select(toDo => new ToDoRequest(toDo));
        }

        public async Task<ToDoRequest> Update(ToDoRequest toDo)
        {

            ToDo toDoDbModel = await _engineeringDashboardDbContext.ToDoItems.FindAsync(toDo.Id);
            if (toDoDbModel != null)
            {
                _engineeringDashboardDbContext.ToDoItems.Update(toDoDbModel);
                await _engineeringDashboardDbContext.SaveChangesAsync();
            }
            return new ToDoRequest(toDoDbModel);
        }

        public async Task Delete(int id)
        {
            var toDo = await _engineeringDashboardDbContext.ToDoItems.FindAsync(id);
            if (toDo != null)
            {
                _engineeringDashboardDbContext.ToDoItems.Remove(toDo);
                await _engineeringDashboardDbContext.SaveChangesAsync();
            }
        }
    }
}

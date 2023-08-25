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
using IdGen;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
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
            //using (var context = new dbcontext())
            //{
            ToDo toDo = await _engineeringDashboardDbContext.ToDoItems.FindAsync(id);
            return new ToDoRequest(toDo);
        }
        //public async Task<ToDoRequest> Add(ToDoRequest toDo)
        //{
        //    int count = await _engineeringDashboardDbContext.ToDoItems.CountAsync();
        //    ////var generator = new IdGenerator(0);
        //    //var id = generator.CreateId();

        //    ToDo toDoDbModel = new ToDo(toDo);
        //    toDoDbModel.DueDate = toDoDbModel.DueDate.ToUniversalTime();

        //    //toDoDbModel.UserLogin = _engineeringDashboardDbContext.UserLogins.FirstOrDefault();
        //    //toDoDbModel.UserLoginId=1;
        //    //toDoDbModel.Id = count+1;
        //    await _engineeringDashboardDbContext.ToDoItems.AddAsync(toDoDbModel);
        //    await _engineeringDashboardDbContext.SaveChangesAsync();
        //    return new ToDoRequest(toDoDbModel);
        //}

    public async Task<ToDoRequest> Add(ToDoRequest toDo)
    {
        ToDo toDoDbModel = new ToDo(toDo);
        if (long.TryParse(toDo.Id, out long longId))
        {
            toDoDbModel.Id = longId;
        }
        else
        {
            throw new ArgumentException("Invalid ID format in the request.");
        }

        toDoDbModel.DueDate = toDoDbModel.DueDate.ToUniversalTime();

        await _engineeringDashboardDbContext.ToDoItems.AddAsync(toDoDbModel);
        await _engineeringDashboardDbContext.SaveChangesAsync();

        toDo.Id = toDoDbModel.Id.ToString();
        return toDo;
    }

        public async Task DeleteAllCompletedAsync(int userLoginId)
        {
            var completedTasks = _engineeringDashboardDbContext.ToDoItems
                .Where(todo => todo.UserLoginId == userLoginId && todo.IsCompleted);

            _engineeringDashboardDbContext.ToDoItems.RemoveRange(completedTasks);
            await _engineeringDashboardDbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<ToDoRequest>> GetAll()
        {
            List<ToDo> toDoItems = await _engineeringDashboardDbContext.ToDoItems.ToListAsync();
            return toDoItems.Select(toDo => new ToDoRequest(toDo));
        }

        public async Task<ToDoRequest> Update(ToDoRequest toDo)
        {
            //ToDo toDoDbModel = await _engineeringDashboardDbContext.ToDoItems.FindAsync(toDo.Id);
            //if (toDoDbModel != null)
            //{
            //    toDoDbModel.Title = toDo.Title;
            //    toDoDbModel.Description = toDo.Description;
            //    toDoDbModel.DueDate = toDo.DueDate;
            //    toDoDbModel.IsCompleted = toDo.IsCompleted;
            //    //toDoDbModel.UserLoginId = toDo.UserLoginId;

            //    _engineeringDashboardDbContext.ToDoItems.Update(toDoDbModel);
            //    await _engineeringDashboardDbContext.SaveChangesAsync();
            //}

            //return new ToDoRequest(toDoDbModel);
            return null;
        }

        public async Task<ToDoRequest> CompleteTask(long id)
        {
            ToDo toDoDbModel = await _engineeringDashboardDbContext.ToDoItems.FindAsync(id);
            if (toDoDbModel != null)
            {
                toDoDbModel.IsCompleted = true;

                _engineeringDashboardDbContext.ToDoItems.Update(toDoDbModel);
                await _engineeringDashboardDbContext.SaveChangesAsync();
            }

            return new ToDoRequest(toDoDbModel);
        }



        public async Task Delete(long id)
        {
            var toDo = await _engineeringDashboardDbContext.ToDoItems.FindAsync(id);
            if (toDo != null)
            {
                _engineeringDashboardDbContext.ToDoItems.Remove(toDo);
                await _engineeringDashboardDbContext.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<ToDoRequest>> GetByUserLoginId(string email)
        {
            // Get the userLoginId based on the provided userEmail
            int? userLoginId = _engineeringDashboardDbContext.UserLogins
                .Where(u => u.Email == email)
                .Select(u => (int?)u.Id)
                .FirstOrDefault();

            if (userLoginId.HasValue)
            {
                // Return the To Do items for the userLoginId
                List<ToDo> toDoItems = await _engineeringDashboardDbContext.ToDoItems.Where(toDo => toDo.UserLoginId == userLoginId.Value).ToListAsync();
                return toDoItems.Select(toDo => new ToDoRequest(toDo));
            }

            // If the userLoginId is not found, return an empty list 
            return new List<ToDoRequest>();
        }
    }
}

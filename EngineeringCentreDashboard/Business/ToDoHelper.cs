using EngineeringCentreDashboard.Data;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineeringCentreDashboard.Business
{
    public class ToDoHelper : IToDoHelper
    {
        private readonly ToDoDbContext _toDoDbContext;

        public ToDoHelper(ToDoDbContext toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task<ToDo> Get(int id)
        {
            return await _toDoDbContext.ToDoItems.FindAsync(id);
        }

        public async Task<ToDo> Add(ToDo toDo)
        {
            await _toDoDbContext.ToDoItems.AddAsync(toDo);
            await _toDoDbContext.SaveChangesAsync();
            return toDo;
        }

        public async Task<IEnumerable<ToDo>> GetAll()
        {
            return await _toDoDbContext.ToDoItems.ToListAsync();
        }

        public async Task<ToDo> Update(ToDo toDo)
        {
            _toDoDbContext.ToDoItems.Update(toDo);
            await _toDoDbContext.SaveChangesAsync();
            return toDo; 
        }

        public async Task Delete(int id)
        {
            var toDo = await _toDoDbContext.ToDoItems.FindAsync(id);
            if (toDo != null)
            {
                _toDoDbContext.ToDoItems.Remove(toDo);
                await _toDoDbContext.SaveChangesAsync();
            }
        }
    }
}


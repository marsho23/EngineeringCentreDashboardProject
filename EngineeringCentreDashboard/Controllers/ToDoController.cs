using EngineeringCentreDashboard.Filters;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;

namespace EngineeringCentreDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoHelper _helper;
        public ToDoController(IToDoHelper helper)
        {
            _helper = helper;
        }

        [HttpPost]
        [ValidateModelState]
        [Route("add")]
        public IActionResult Add([FromBody] ToDo toDo)
        {
            _helper.Add(toDo);
            return Ok(toDo);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(int id)
        {
            ToDo toDo = await _helper.Get(id);
            if (toDo == null)
            {
                return NotFound();
            }
            return Ok(toDo);
        }


        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ToDo> toDoList = await _helper.GetAll();
            return Ok(toDoList);
        }


        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(int id, [FromBody] ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return BadRequest();
            }

            ToDo updatedToDo = await _helper.Update(toDo);
            if (updatedToDo == null)
            {
                return NotFound();
            }

            return Ok(updatedToDo);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            _helper.Delete(id);
            return Ok(id);
        }
    }
}

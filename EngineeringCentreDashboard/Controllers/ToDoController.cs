using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Filters;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Models.Request;
using IdGen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace EngineeringCentreDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ToDoController : ControllerBase
    {
        private readonly IToDoHelper _helper;
        //private readonly UserLoginHelper _userLoginHelper;

        public ToDoController(IToDoHelper helper)
        {
            _helper = helper;
            //_userLoginHelper = new UserLoginHelper();
        }

        [HttpPost]
        [ValidateModelState]
        [Route("add")]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Add([FromBody] ToDoRequest toDo)
        {
            //int userLoginId =(int) toDo.UserLoginId; 
            //UserLogin userLogin = _userLoginHelper.GetUserLoginById(userLoginId); 

            //toDo.UserLogin = userLogin;
            var generator = new IdGenerator(0);
            var id = generator.CreateId();
            toDo.Id = id.ToString();
            _helper.Add(toDo);
            return Ok(toDo);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(int id)
        {
            ToDoRequest toDo = await _helper.Get(id);
            if (toDo == null)
            {
                return NotFound();
            }
            return Ok(toDo);
        }


        [HttpGet]
        [Route("getall")]
        [EnableCors("AllowAllOrigins")]

        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ToDoRequest> toDoList = await _helper.GetAll();
            return Ok(toDoList);
        }

        [HttpGet]
        [Route("getByUserLoginId")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetByUserLoginId(string email)
        {

            IEnumerable<ToDoRequest> toDoItems =await _helper.GetByUserLoginId(email);

            //var toDoRequests = toDoItems.Select(toDo => new ToDoRequest(toDo)).ToList();
            return Ok(toDoItems);
        }

        //[HttpPut]
        //[Route("update")]
        //public async Task<IActionResult> Update(int id, [FromBody] ToDoRequest toDo)
        //{
        //    if (id != toDo.Id)
        //    {
        //        return BadRequest();
        //    }

        //    ToDoRequest updatedToDo = await _helper.Update(toDo);
        //    if (updatedToDo == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(updatedToDo);
        //}

        [HttpPut]
        [Route("completeTask")]
        public async Task<IActionResult> CompleteTask(string id)
        {
            long.TryParse(id, out long longId);
            ToDoRequest updatedToDo = await _helper.CompleteTask(longId);
            if (updatedToDo == null)
            {
                return NotFound();
            }

            return Ok(updatedToDo);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            long.TryParse(id, out long longId);

            await _helper.Delete(longId);
            return Ok(id);
        }

        [HttpDelete]
        [Route("deleteAllCompleted")]
        public async Task<IActionResult> DeleteAllCompleted(int userLoginId)
        {
            await _helper.DeleteAllCompletedAsync(userLoginId);
            return Ok();
        }

    }
}

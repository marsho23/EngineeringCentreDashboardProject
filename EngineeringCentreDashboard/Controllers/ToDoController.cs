using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Filters;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Models.Request;
using Microsoft.AspNetCore.Cors;
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
        //private readonly UserLoginHelper _userLoginHelper;

        public ToDoController(IToDoHelper helper)
        {
            _helper = helper;
            //_userLoginHelper = new UserLoginHelper();
        }

        [HttpPost]
        [ValidateModelState]
        [Route("add")]
        public IActionResult Add([FromBody] ToDoRequest toDo)
        {
            //int userLoginId =(int) toDo.UserLoginId; 
            //UserLogin userLogin = _userLoginHelper.GetUserLoginById(userLoginId); 

            //toDo.UserLogin = userLogin;

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


        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(int id, [FromBody] ToDoRequest toDo)
        {
            if (id != toDo.Id)
            {
                return BadRequest();
            }

            ToDoRequest updatedToDo = await _helper.Update(toDo);
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

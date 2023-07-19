using EngineeringCentreDashboard.Filters;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineeringCentreDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserLoginHelper _helper;

        public UserLoginController(IUserLoginHelper helper)
        {
            _helper = helper;
        }

        [HttpPost]
        [ValidateModelState]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] UserLogin userLogin)
        {
            var addedUserLogin = await _helper.Add(userLogin);
            return Ok(addedUserLogin);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userLogin = await _helper.Get(id);
            if (userLogin == null)
            {
                return NotFound();
            }

            return Ok(userLogin);
        }

        [HttpGet]
        [Route("getall")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetAll()
        {
            var userLogins = await _helper.GetAll();
            return Ok(userLogins);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserLogin userLogin)
        {
            if (id != userLogin.Id)
            {
                return BadRequest();
            }

            var updatedUserLogin = await _helper.Update(userLogin);
            if (updatedUserLogin == null)
            {
                return NotFound();
            }

            return Ok(updatedUserLogin);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _helper.Delete(id);
            return Ok(id);
        }
    }
}

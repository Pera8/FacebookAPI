using FacebookApiTest.Repository;
using FacebookApiTest.Repository.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Threading.Tasks;

namespace FacebookApiTest.Controllers
{
    [Route("api/Users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly UsersService usersService;
        public UserController(UsersService usersService)
        {
            this.usersService = usersService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await usersService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return await usersService.GetAsyncById(id);
        }
    }
}

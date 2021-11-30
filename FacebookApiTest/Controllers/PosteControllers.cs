using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service;
using Shared.DTO;
using System.Threading.Tasks;

namespace FacebookApiTest.Controllers
{
    [Route("api/Post")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PosteController: ControllerBase
    {
        private readonly PostesService postesService;
        public PosteController(PostesService postesService)
        {
            this.postesService = postesService;
        }

        [HttpGet]
        
        public async Task<ActionResult> GetAllPosts()
        {
            return Ok(await postesService.GetAllWithComment());
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody] PostDto poste )
        {           
            return Ok( await postesService.AddAsync(poste));
        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Poste>> GetPost(int id)
        {
            return await postesService.GetAsyncById(id);           
           
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePost(int id)
        {           
            await postesService.DeleteAsync(id);
            return Ok($"Poste with id={id} delete");
        }
    }
}

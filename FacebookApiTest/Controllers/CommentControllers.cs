using FacebookApiTest.Repository.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service;
using Shared.DTO;
using System.Threading.Tasks;

namespace FacebookApiTest.Controllers
{
    [Route("api/Comments")]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentController: ControllerBase
    {
        private readonly CommentsService commentService;
        public CommentController(CommentsService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllComment()
        {
            return Ok(await commentService.GetAll());
        }

        [HttpPost]           
        public async Task<ActionResult> CreateCommet([FromBody] CommentDto comment)
        {           
            return Ok(await commentService.AddAsync(comment));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            return await commentService.GetAsyncById(id);     
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteComment(int id)
        {            
            await commentService.DeleteAsync(id);
            return Ok();
        }
    }
}

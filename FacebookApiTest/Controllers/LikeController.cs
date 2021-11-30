using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookApiTest.Controllers
{
    [Route("api/Likes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]    
        public async Task<ActionResult> CreateLike(LikeDto like)
        {            
            return Ok( await _likeService.AddLike(like));
            // CreatedAtAction(nameof(GetAllLikes), new { id = createLike.PosteId }, createLike);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLike(int id)
        {
            await _likeService.DeleteLike(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Like>> GetLike(int id)
        {
            return await _likeService.GetLike(id);                       
        }

        [HttpGet]
        public async Task<ActionResult> GetAllLikes()
        {
            return Ok(await _likeService.GetAllLike());
        }
    }
}

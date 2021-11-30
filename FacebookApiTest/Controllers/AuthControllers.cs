using FacebookApiTest.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly AuthTokenService authTokenService;
        private readonly AuthService authService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, AuthTokenService authTokenService, AuthService authService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authTokenService = authTokenService;
            this.authService = authService;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var result = await authService.RegisterUserAuth(registerUser);
            return Ok(result);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginUser loginParam)
        {
            var result = await authService.LoginUserAuth(loginParam);                          
            return Ok(result);                        
        }

        [HttpPost(nameof(ForgotPassword))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPasswordDto)
        {
            await authService.ForgotPasswordAuth(forgotPasswordDto);            
            return Ok();

        }

        [HttpPost(nameof(CheckIfTokenExpierd))]
        public IActionResult CheckIfTokenExpierd([FromBody] TokenExpierdDto tokenExpierd)
        {
            return Ok(authTokenService.VerifyTokenAsync(tokenExpierd.Code, tokenExpierd.UserId).Result);
        }

        [HttpPost(nameof(ResetPassword))]
        public IActionResult ResetPassword([FromBody] ResetPassword resetPasswordDto, string userId, string code)
        {
            var result = authService.ResetPasswordAuth( resetPasswordDto,  userId,  code);            
            return Ok();
        }

    }
}

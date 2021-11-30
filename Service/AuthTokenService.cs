using FacebookApiTest.Repository;
using FacebookApiTest.Repository.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Service.Communication;
using Service.Configuration;
using Shared.AuthConstants;
using Shared.DTO;
using Shared.Helper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthTokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        static AuthTokenService() => MapperConfig.RegisterUserMapping();


        public AuthTokenService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IOptions<AppSettings> appSettings,
            IEmailService emailservice)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _emailService = emailservice;
        }
        public async Task<AuthDto> Authenticate(string email, string password, bool remeberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && user.EmailConfirmed)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);
                if (result.Succeeded)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var expireTime = remeberMe ? _appSettings.JwtDurationInHoursLong : _appSettings.JwtDurationInHoursShort;
                    JwtSecurityToken tokenOptions;

                    tokenOptions = new JwtSecurityToken(
                                            issuer: _appSettings.BaseUrl,
                                            audience: _appSettings.BaseUrl,
                                            expires: DateTime.Now.AddHours(expireTime),
                                            signingCredentials: signinCredentials
                                        );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    var auth = new AuthDto { Token = tokenString, UserAuth = TypeAdapter.Adapt<User, UserAuth>(user) };
                    return await Task.FromResult(auth);
                }
            }

            return null;
        }

        public async Task ForgotPasswordAsync(string email)
        {
            await SendSetPasswordMail(email, true);
        }

        public async Task ResetPassword(string userId, string code, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var response = await _userManager.ResetPasswordAsync(user, Base64Helper.Base64Decode(code), password);
            if (user != null && response.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }
        }

        //helpers
        //sent user resset password conformation mail

        public async Task SendSetPasswordMail(string emailAddress, bool isAccountActivated)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress);
            var codeBase = await _userManager.GeneratePasswordResetTokenAsync(user);
            var code = Base64Helper.Base64Encode(codeBase);

            var callbackUrl = string.Format(AuthConstants.UserResetPassConfirmationLink, _appSettings.BaseClientUrl, user.Id, code, isAccountActivated);

            string emailBody = "Hi " + user.UserName + "<br/>click the link below to set your password<br/>" + "<a href=" + callbackUrl + ">Click here</a>";

            var email = _emailService.CreateMail(_appSettings.SenderEmail, _appSettings.SenderName, "Set password", emailBody,
                user.Email);

            await _emailService.Execute(email);
        }

        public async Task<bool> CheckEmailUnique(string email)
        {
            var userByEmail = await _userManager.FindByEmailAsync(email);
            return userByEmail == null;
        }

        public async Task<bool> VerifyTokenAsync(string code, int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var codeVer = Base64Helper.Base64Decode(code);
            return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", codeVer);
        }

        
    }
}

using System;
using AutoMapper;
using MamApi.Models;
using MamApi.Models.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MamApi.Services;

namespace MamApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IMapper mapper, 
            IConfiguration configuration)
        {
            _authService = authService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test() {
            return Ok("Test Authorize OK");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginResource loginResource)
        {
            try
            {
                if (loginResource == null)
                    return BadRequest("ข้อมูลในการ Login ไม่ถูกต้อง");

                var user = _authService.CheckCredential(loginResource.UserId, loginResource.Password);

                if (user == null)
                    return NotFound("User Id หรือ Password ไม่ถูกต้อง");

                var userProfileResource = _mapper.Map<User, UserProfileResource>(user);

                long currentLoginId = _authService.SaveCurrentLogin(loginResource);

                // ไว้ค่อย Check IMIE ใน DB อีกที
                userProfileResource.IMIE = loginResource.IMEI;

                var token = _authService.CreateToken(userProfileResource, 
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:PrivateKey"]);

                var loginResult = new { Token = token, User = userProfileResource };

                return Ok(loginResult);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Login : Error occurs => {ex} ");
            }

        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutResource logoutResource)
        {
            if (logoutResource == null)
                return BadRequest(new ErrorMessage { ErrorText = "ข้อมูล Logout ไม่ถูกต้อง" });

            if (string.IsNullOrEmpty(logoutResource.UserId))
            {
                return BadRequest(new ErrorMessage { ErrorText = "โปรดระบุ User Id" });
            }

            DateTime logoutTime = _authService.Logout(logoutResource.UserId);

            return Ok($"Logout Succeed : { logoutTime }");
        }
    }
}
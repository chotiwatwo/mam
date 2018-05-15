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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginResource loginResource)
        {
            try
            {
                if (loginResource == null)
                    return BadRequest("ข้อมูลในการ Login ไม่ถูกต้อง");

                var user = _authService.CheckCredential(loginResource.Username, loginResource.Password);

                if (user == null)
                    return NotFound("Username หรือ Password ไม่ถูกต้อง");

                var userProfileResource = _mapper.Map<User, UserProfileResource>(user);

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

                throw;
                
            }

            return BadRequest("Fail to generate token");
        }
        
    }
}
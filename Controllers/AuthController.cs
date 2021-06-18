using C.Tools.Security.Jwt.Services;
using GestProject.WebService.Dto;
using GestProject.WebService.Dto.Forms;
using GestProject.WebService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        private readonly JwtService _tokenService;

        public AuthController(AuthService service, JwtService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterFormDto form)
        {
            try
            {
                _service.Register(form);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            };
        }


        [HttpPost("login")]
        [Produces("application/json", Type = typeof(string))]
        public IActionResult Login(LoginFormDto form)
        {
            PayloadDto user = _service.Login(form);
            if (user != null)
            {
                string token = _tokenService.Encode(user);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}

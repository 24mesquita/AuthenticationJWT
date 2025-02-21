using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JWT;
using AuthenticationJWT.Services;
using Newtonsoft.Json.Linq;
using AuthenticationJWT.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationJWT.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]LoginRequest loginRequest)
        {
            try
            {
               string token = _authService.LoginUser(loginRequest.Username, loginRequest.Password);
                if (token != null) return Ok(token);
                return Unauthorized("User or password incorrect");

            }catch(Exception ex)
            {
                return StatusCode(500, "Internal server error: "+ ex.Message);
            }
        }

    }
}


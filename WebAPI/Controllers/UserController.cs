﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class UserController : Controller
    {
        private readonly IConfiguration Configuration;

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static List<User> AllUsers = new List<User>
        {
            new User(1, "david", "david2022"),
            new User(2, "agustin", "agustin2022"),
        };

        /// <summary>
        /// Logueo de usuario
        /// </summary>
        /// <response code="200">Exito</response>
        /// <response code="400">Peticion incorrecta</response>
        /// <response code="500">Error del servidor</response>
        [AllowAnonymous]
        [HttpPost("login")]
        public JsonResult Login(string username, string password)
        {
            try
            {
                var user = AllUsers.FirstOrDefault(x => x.Username == username && x.Password == password);
                if (user != null)
                {
                    var token = BuildJWTToken();
                    SetTokenCookie(token);
                    return new JsonResult(token);
                }
                var result = new JsonResult("Credenciales incorrectas");
                result.StatusCode = 400;
                return result;
            }
            catch (Exception err)
            {
                return new JsonResult("Hubo un error al loguear el usuario. " + err);
            }
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["token"];
            return Ok(refreshToken);
        }

        private string BuildJWTToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = Configuration["AppSettings:Issuer"];
            var audience = Configuration["AppSettings:Audience"];
            var jwtValidity = DateTime.Now.AddMinutes(Convert.ToDouble(Configuration["JWT:TokenExpiry"]));

            var token = new JwtSecurityToken(issuer,
              audience,
              expires: jwtValidity,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SetTokenCookie(string token)
        {
            CookieOptions cookieOptions = new ()
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            if (Response != null)
            {
                Response.Cookies.Append("token", token, cookieOptions);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using GestioneMagazzino.Middleware;
using GestioneMagazzino.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestioneMagazzino.Controllers
{
    public class AuthController : Controller
    {
        // GET: /<controller>/
        public IActionResult LogIn()
        {

            return View();
        }
        [HttpPost()]
        public IActionResult DoLogin([FromBody] UserViewModel user)
        {
            if (user.Password != "123") return Json("Password errata");
            var requestAt = DateTime.UtcNow;
            var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
            var token = "";

            var handler = new JwtSecurityTokenHandler();

            var identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "TokenAuth"),
                new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Username", user.Username),
                    new Claim("LastName", user.Username),
                    new Claim("Role", "Admin")

                }
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expiresIn
            });

            token = handler.WriteToken(securityToken);
            //generiamo l'identity
            if (string.IsNullOrEmpty(token)) return Json("Impossibile generare il token!!!!!!");
            Response.Cookies.Append("Authorization", $"Bearer {token}");
            return Json(new { status = "OK", token });

        }
        [Authorize("Bearer")]
        public IActionResult LogOut()
        {

            return View();
        }
        public IActionResult Register()
        {

            return View();
        }
        [Authorize("Bearer")]
        public IActionResult Test()
        {
        
            return Json(Enumerable.Range(0,100));
        }
    }
}

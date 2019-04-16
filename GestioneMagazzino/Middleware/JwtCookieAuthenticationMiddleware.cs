using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GestioneMagazzino.Middleware
{
    public class JwtCookieAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public static string LoginPagePath;

        public JwtCookieAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //controlla se è stato fatto un login quindi se c'è un cookie Authorization
            var token = context.Request.Cookies["Authorization"];

            //se è un umano da browser
            if (string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]) && !string.IsNullOrWhiteSpace(token))
            {
                context.Request.Headers["Authorization"] = token;
            }

            
            await _next(context);
            if (context.Response.StatusCode == 401) //se non è autorizzato
            {
                //se è un AJAX o è di tipo JSON (API)
                if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest"  || context.Request.ContentType == "application/json")
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { authenticated = false, message = "token empty or invalid" }));
                }
                else
                {
                    //torna alla LogIn
                    context.Response.Redirect($"{LoginPagePath}?redirect={context.Request.Path}");
                }
            }
           
        }

       


    }
}

using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace GestioneMagazzino.Middleware
{
    public static class JwtCookieAuthMiddlewareExtensions
    {
        public static IApplicationBuilder EnableJwtCookieAuthentication(this IApplicationBuilder app, string loginPagePath)
        {

            JwtCookieAuthenticationMiddleware.LoginPagePath = loginPagePath;

            return app.UseMiddleware<JwtCookieAuthenticationMiddleware>();

        }
        public static void SetTokenValidationParameters(JwtBearerOptions jwtBearerOptions)
        {
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = TokenAuthOption.Key,
                ValidAudience = TokenAuthOption.Audience,
                ValidIssuer = TokenAuthOption.Issuer,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(0)
            };
        }


    }
}
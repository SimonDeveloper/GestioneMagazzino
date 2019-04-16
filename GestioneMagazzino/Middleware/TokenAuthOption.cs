using System;
using Microsoft.IdentityModel.Tokens;

namespace GestioneMagazzino.Middleware
{
    public class TokenAuthOption
    {
        public static string Issuer { get; } = "https://api.motstudios.it";
        public static string Audience { get; } = "MotUser";
        public static RsaSecurityKey Key { get; set; } = RsaKeyHelper.GenerateKey();
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(200);
    }
}
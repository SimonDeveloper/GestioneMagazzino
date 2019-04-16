using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace GestioneMagazzino.Middleware
{
    public class RsaKeyHelper
    {
        public static RsaSecurityKey GenerateKey()
        {
            RsaSecurityKey res;
            using (var key = RSA.Create())
            {
                key.KeySize = 2048;
                res = new RsaSecurityKey(key.ExportParameters(true));
            }
            return res;
        }
    }
}
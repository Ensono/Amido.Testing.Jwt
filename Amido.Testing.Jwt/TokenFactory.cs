using System;
using System.Text;
using Amido.Testing.Jwt.Models;
using Amido.Testing.Jwt.Services;

namespace Amido.Testing.Jwt
{
    public class TokenFactory
    {
        private readonly JwtGeneratorService jwtGeneratorService;

        public TokenFactory()
        {
            jwtGeneratorService = new JwtGeneratorService();
        }

        public string CreateJwtToken(JwtTokenProperties jwtTokenProperties, bool base64Encode = true)
        {
            var token = jwtGeneratorService.CreateJwtToken(jwtTokenProperties);

            if (base64Encode)
            {
                return Base64Encode(token.ToString());
            }

            return token.ToString();
        }

        private static string Base64Encode(string token)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            return Convert.ToBase64String(encoding.GetBytes(token));
        }
    }
}

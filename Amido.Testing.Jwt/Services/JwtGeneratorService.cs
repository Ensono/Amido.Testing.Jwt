using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using Amido.Testing.Jwt.Models;

namespace Amido.Testing.Jwt.Services
{
    public class JwtGeneratorService
    {
        public JwtSecurityToken CreateJwtToken(JwtTokenProperties jwtTokenProperties)
        {
            var claims = jwtTokenProperties.Claims.Select(claim => new Claim(claim.Key, claim.Value)).ToList();

            var token = new JwtSecurityToken(jwtTokenProperties.Issuer, jwtTokenProperties.Audience, claims, new Lifetime(jwtTokenProperties.CreatedDate, jwtTokenProperties.ExpiryDate), new X509SigningCredentials(jwtTokenProperties.X509Certificate2));

            return token;
        }
    }
}
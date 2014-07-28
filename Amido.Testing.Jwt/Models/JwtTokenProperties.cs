using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Amido.Testing.Jwt.Models
{
    public class JwtTokenProperties
    {
        public JwtTokenProperties()
        {
            Claims = new Dictionary<string, string>();
        }

        public string NameIdentifier { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public DateTime CreatedDate { get; set; }
 
        public DateTime ExpiryDate { get; set; }

        public X509Certificate2 X509Certificate2 { get; set; }

        public Dictionary<string, string> Claims { get; set; } 
    }
}
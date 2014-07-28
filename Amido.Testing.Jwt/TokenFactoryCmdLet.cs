using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Windows.Forms;
using Amido.Testing.Jwt.Certificates;
using Amido.Testing.Jwt.Models;
using Newtonsoft.Json;

namespace Amido.Testing.Jwt
{
    [Cmdlet(VerbsCommon.Get, "JwtToken")]
    public class TokenFactoryCmdLet : PSCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The trusted issuer with which the token will be created with")]
        [Alias("i")]
        public string Issuer { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "The trusted audience for which the token will be used with")]
        [Alias("a")]
        public string Audience { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "The thumbprint of the certificate to sign the token with")]
        [Alias("t")]
        public string Thumbprint { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "The store name of the certificate to sign the token with")]
        public string StoreName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "The store location of the certificate to sign the token with")]
        public string StoreLocation { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "If true the output will be Base64 encoded.  Default is true")]
        public string Base64Encode { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The start date and time of the token lifetime.  Default is 24 hours in the past")]
        public string CreatedDate { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The end date and time of the token lifetime.  Default is 24 hours in the future")]
        public string ExpiryDate { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "A JSON string representing the claims that will be added to JWT token")]
        public string Claims { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                var certificate = CertificateHelper.FindByThumbprint(Thumbprint, CertificateHelper.GetStoreName(StoreName), CertificateHelper.GetStoreLocation(StoreLocation));

                var jwtTokenProperties = new JwtTokenProperties
                {
                    Issuer  = Issuer,
                    Audience = Audience,
                    X509Certificate2 = certificate,
                    CreatedDate = GetDateFromInput(CreatedDate, -24), 
                    ExpiryDate = GetDateFromInput(ExpiryDate, 24), 
                    Claims = ParseClaims()
                };

                var jwtToken = new TokenFactory().CreateJwtToken(jwtTokenProperties);

                WriteObject(jwtToken);

                Clipboard.SetText(jwtToken);

                WriteVerbose("JWT Token generated and added to the clipboard");
            }
            catch (Exception exception)
            {
                WriteError(new ErrorRecord(exception, "1", ErrorCategory.InvalidOperation, string.Format("An exception has been thrown generating the JWT token")));
            }

            base.ProcessRecord();
        }

        private DateTime GetDateFromInput(string value, int defaultHours)
        {
            if (string.IsNullOrEmpty(value) || !ParseDateTime(value))
            {
                return DateTime.UtcNow.AddHours(defaultHours);
            }

            return Convert.ToDateTime(value);
        }

        private bool ParseDateTime(string dateTime)
        {
            DateTime parseDateTime;

            if (DateTime.TryParse(dateTime, out parseDateTime))
            {
                return true;
            }

            return false;
        }

        private Dictionary<string, string> ParseClaims()
        {
            try
            {
                if (string.IsNullOrEmpty(Claims))
                {
                    return new Dictionary<string, string>();
                }

                return JsonConvert.DeserializeObject<Dictionary<string, string>>(Claims);
            }
            catch (Exception exception)
            {
                WriteError(new ErrorRecord(exception, "2", ErrorCategory.InvalidData, string.Format("Error parsing JSON collection on claims")));
            }

            return new Dictionary<string, string>();
        }
    }
}
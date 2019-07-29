using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace RestWithAspNETUdemy.Security.Configuration
{
    public class SignInConfiguration
    {
        public SecurityKey Key { get; }
        public SigningCredentials Credentials { get; }

        public SignInConfiguration()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            Credentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
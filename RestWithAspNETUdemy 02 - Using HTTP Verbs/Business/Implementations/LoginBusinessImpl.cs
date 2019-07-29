
using System;
using RestWithAspNETUdemy.Model;
using RestWithAspNETUdemy.Repository;
using RestWithAspNETUdemy.Security.Configuration;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace RestWithAspNETUdemy.Business.Implementations
{
    public class LoginBusinessImpl : ILoginBusiness
    {
        private IUserRepository _respository;
        private SignInConfiguration _signin;
        private TokenConfiguration _token;

        public LoginBusinessImpl(IUserRepository repository, SignInConfiguration signIn, TokenConfiguration token)
        {
            _respository = repository;
            _signin = signIn;
            _token = token;
        }

        public object FindByLogin(User login)
        {
            bool credentialValid = false;
            if (login != null && !string.IsNullOrWhiteSpace(login.Login))
            {
                var baseUser = _respository.FindByLogin(login.Login);
                credentialValid = (baseUser != null && login.Login == baseUser.Login && login.AccessKey == baseUser.AccessKey);
            }
            if (credentialValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(login.Login, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, login.Login)
                    }
                );
                var createDate = DateTime.Now;
                var expirationDate = createDate + TimeSpan.FromSeconds(_token.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string tokenValue = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, tokenValue);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                SigningCredentials = _signin.Credentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                authenticated = false,
                message = "Fail to authenticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
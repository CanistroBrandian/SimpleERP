using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleERP.Helpers
{
    public static class AuthHelper
    {
        private const string _KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        private const string _TOKEN_ISSUER = "SimpleERP"; // издатель токена
        private const string _TOKEN_AUDIENCE = "https://localhost:44332/"; // потребитель токена'
        private const int _TOKEN_LIFETIME = 60; // время жизни токена - 60 минут

        public const string SUPERVISOR_ROLE = "Supervisor";

        public static JwtSecurityToken GetJWT(ClaimsIdentity identity)
        {
            var jwt = new JwtSecurityToken(
                            issuer: _TOKEN_ISSUER,
                            audience: _TOKEN_AUDIENCE,
                            notBefore: DateTime.UtcNow,
                            claims: identity.Claims,
                            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_TOKEN_LIFETIME)),
                            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey()
                            , SecurityAlgorithms.HmacSha256));
            return jwt;
        }

        public static string EncodeJWT(JwtSecurityToken jwt)
        {
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static TokenValidationParameters BuildTokenValidationParameters()
        {
           return new TokenValidationParameters
            {
                // укзывает, будет ли валидироваться издатель при валидации токена
                ValidateIssuer = true,
                // строка, представляющая издателя
                ValidIssuer = _TOKEN_ISSUER,
                // будет ли валидироваться потребитель токена
                ValidateAudience = true,
                // установка потребителя токена
                ValidAudience = _TOKEN_AUDIENCE,
                // будет ли валидироваться время существования
                ValidateLifetime = true,
                // установка ключа безопасности
                IssuerSigningKey = GetSymmetricSecurityKey(),
                // валидация ключа безопасности
                ValidateIssuerSigningKey = true
            };
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_KEY));
        }
    }
}

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Servicios.api.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.JwtLogic
{
    public class JwtGenerate : IJwtGenerate
    {
        private readonly IDistributedCache _cache;

        public JwtGenerate(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task CancelToken(string token)
        {
            /// TODO: Pendiente por implementar
            //await _cache.SetStringAsync(token, " ", new DistributedCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            //});
        }

        public async Task<bool> IsActiveToken(string token) {
            /// TODO: Pendiente por implementar
            //var jwtSecurityHandler = new JwtSecurityTokenHandler();
            //var jwtSecurityToken = jwtSecurityHandler.ReadJwtToken(token);            
            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    ValidateAudience = false,
            //    ValidateIssuer = false,
            //    //LifetimeValidator = (before, expires, token, parameters) =>
            //    //{
            //    //    if (before.HasValue && before.Value > DateTime.Now)
            //    //        return false;

            //    //    if (expires.HasValue)
            //    //        return expires.Value > DateTime.Now;

            //    //    return true;
            //    //},
            //    //ValidateLifetime = true,
            //    IssuerSigningKey = GetSecurityKey()
            //};
            //var claimsPrincipal = await jwtSecurityHandler.ValidateTokenAsync(token, tokenValidationParameters);
            //var result = await _cache.GetStringAsync(token);
            return true;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("username", user.UserName),
                new Claim("name", user.Name),
                new Claim("surnames", user.Surnames),
                new Claim("typeuser", user.TypeUser)
            };
            var key = GetSecurityKey();
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credential
            };

            var jwtSecurityHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityHandler.CreateToken(tokenDescription);
            return jwtSecurityHandler.WriteToken(token);
        }

        private SymmetricSecurityKey GetSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qOp0oyOTiDwrB0ul5M2Rw1N421EI1lgi"));
    }
}

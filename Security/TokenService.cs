using CineMoodAI.Aplication.interfaces;
using CineMoodAI.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CineMoodAI.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));  //appsetting tek, keyi aldık
        }

        public string CreateToken(AppUser user) //createtoken da artık claim olarak ne donmek isterse arayuze
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id), //userıd ve username donmek istemisiz
                new(ClaimTypes.Name, user.UserName!)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256); //burda haslameyi yapar

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), //Expair suresi=bu token ne kadar zaman gecerli olcak 
                SigningCredentials = creds
            };
            //burda da bı token yaratıyor
            var tokenMandler = new JwtSecurityTokenHandler();
            var token = tokenMandler.CreateToken(tokenDescriptor);

            return tokenMandler.WriteToken(token);
        }
    }
}

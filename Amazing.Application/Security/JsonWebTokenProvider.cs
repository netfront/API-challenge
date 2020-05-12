using Amazing.Persistence.Enumerators;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Amazing.Application.Security
{
    public interface IJsonWebTokenProvider
    {
        string Generate(int availableDays, List<Claim> claims, EJwtType type);
    }

    public class JsonWebTokenGeneratorProvider : IJsonWebTokenProvider
    {
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="env"></param>
        public JsonWebTokenGeneratorProvider(IConfiguration config)
        {
            this.Configuration = config;
        }

        /// <summary>
        /// Generate a JWT
        /// </summary>
        /// <param name="availableDays">Number of day of availability</param>
        /// <param name="claims">claims to store in the jwt</param>
        /// <param name="type"></param>
        /// <param name="issuer"></param>
        /// <returns></returns>
        public string Generate(int availableDays, List<Claim> claims, EJwtType type)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.Configuration["Tokens:Key"]);
            claims.Add(new Claim(AmazingClaimTypes.TokenType, type.ToString()));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = this.Configuration["Tokens:Audience"],
                Issuer = this.Configuration["Tokens:Issuer"],
                Expires = DateTime.UtcNow.AddDays(availableDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return $"bearer {tokenHandler.WriteToken(token)}";
        }
    }
}
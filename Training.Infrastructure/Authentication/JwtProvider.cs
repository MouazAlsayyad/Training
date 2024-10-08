﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Contracts.Persistence;
using Training.Domain.Entities;

namespace Training.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string Generate(User user)
        {
            var claims = new Claim[]
            {
                new ("Id", user.UserId.ToString()),
                new ("Email", user.Email.ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("Security-Key854796olikujyh0213456")),
                SecurityAlgorithms.HmacSha256);

            DateTime Now = DateTime.UtcNow;
            DateTime Expiration = Now.AddDays(7);

            //var token = new JwtSecurityToken
            //    (
            //        _options.Issueer,
            //        _options.Audience,
            //        claims,
            //        DateTime.UtcNow,
            //        DateTime.UtcNow.AddDays(1),
            //        signingCredentials
            //    );

            var token = new JwtSecurityToken(
                issuer: _options.Issueer,
                audience: _options.Audience,
                claims: claims,
                notBefore: Now,
                expires: Expiration,
                signingCredentials: signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
        public string GetUserIdFromToken(string token)
        {
            var stream = token.Replace("Bearer ", string.Empty);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var Id = tokenS!.Claims.First(claim => claim.Type == "Id").Value;
            return Id;
        }
    }
}

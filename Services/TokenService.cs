using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace ToDo.Service
{
    public static class TokenService
    {
        private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("todotodotodotodotodotodotodotodotodotodotodotodo"));
        private static string issuer = "https://localhost:5001";
        public static SecurityToken GetToken(List<Claim> claims) =>
            new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

        public static  TokenValidationParameters GetTokenValidationParameters() =>
            new TokenValidationParameters
            {
                ValidateIssuer=true,
                ValidateAudience=true,
                ValidateLifetime=true,
                ValidateIssuerSigningKey=true,
            
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero // remove delay of token when expire
            };

        public static string WriteToken(SecurityToken token) =>
            new JwtSecurityTokenHandler().WriteToken(token);
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sixgram.Auth.Common.Roles;
using Sixgram.Auth.Core.Dto.User;
using Sixgram.Auth.Core.Options;
using Sixgram.Auth.Core.Token;

namespace Sixgram.Auth.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;

        public TokenService
        (
            AppOptions appOptions
        )
        {
            _secretKey = appOptions.SecretKey;
        }

        public TokenModel CreateToken(UserModelDto user)
        {
            var token = BuildTokenModel(user);
            return token;
        }

        private TokenModel BuildTokenModel(UserModelDto user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenExpiration = DateTime.Now.AddDays(365);

            var securityToken = new JwtSecurityToken
            (
                claims: GetClaims(user),
                expires: tokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials:
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );

            var token = handler.WriteToken(securityToken);

            return new TokenModel(token, tokenExpiration.Ticks);
        }

        private static IEnumerable<Claim> GetClaims(UserModelDto user)
        {
            var claims = new List<Claim>
            {
                new("id", user.Id.ToString()),
                new("email", user.Email),
            };

            return claims;
        }
    }
}
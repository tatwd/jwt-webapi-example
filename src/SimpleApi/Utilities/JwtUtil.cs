using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Web;

namespace SimpleApi.Utilities
{
    public class JwtUtil
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        private static string _secret = "R0GkJ/bqFwheg32udPuluhmhmscK1jOiW1MGlGFlhGVeo8viIRyqSvC3JL2mrONDEh3JLCOaINOC3VbsqxrRRg==";

        public static string GenerateToken(string username, int expireMinutes = 30)
        {
            var symmetricKey = Convert.FromBase64String(_secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwt == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(_secret);

                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace ConsoleApplication.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        private const string defaultIssuer = "http://sdncast.nl";
        private const string serverSecret = "superdupersecretkey123";

        private readonly SecurityKey serverSecurityKey;
        public JwtProvider()
        {
            serverSecurityKey = new SymmetricSecurityKey(base64UrlDecode(serverSecret));
            Options = new JwtBearerOptions
            {
                TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateAudience = false,
                    ValidIssuer = defaultIssuer,
                    IssuerSigningKey = serverSecurityKey
                }
            };
        }
        public JwtBearerOptions Options { get; }

        private byte[] base64UrlDecode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding
            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default:
                    throw new System.Exception(
             "Illegal base64url string!");
            }
            return Convert.FromBase64String(s); // Standard base64 decoder
        }
        private long toUnixDate(DateTime date)
                => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        public string GenerateToken(string consumerId)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim("consumerid", consumerId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, toUnixDate(now).ToString(), ClaimValueTypes.Integer64)
            };
            var identity = new ClaimsIdentity(claims);
            var signingCredentials = new SigningCredentials(serverSecurityKey, SecurityAlgorithms.HmacSha256);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(defaultIssuer, consumerId, identity, now, now.Add(TimeSpan.FromHours(1)), now, signingCredentials);

            var encodedJwt = handler.WriteToken(token);

            return encodedJwt;
        }
    }
}
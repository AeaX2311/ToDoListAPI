using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ToDoListAPI.Models.Authentication;
using ToDoListAPI.Services.Interface;

namespace ToDoListAPI.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SecurityToken GenerateToken(TokenData tokenData, string secretKeyId)
        {
            var authClaims = new List<Claim>
            {
               new Claim("userId", tokenData.UserId)
            };

            var key = Encoding.ASCII.GetBytes(_configuration[$"authenticationSettings:jwt:{secretKeyId}"]);

            return new JwtSecurityTokenHandler().CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow
            });
        }

        public TokenData ValidateToken(string token, string secretKeyId)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration[$"AuthenticationSettings:JWT:{secretKeyId}"]);
            TokenData tokenResult = new();

            try
            {
                var jwtSecurityToken = handler.ReadJwtToken(token);
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                tokenResult.HasError = false;
                tokenResult.UserId = jwtToken.Claims.First(x => x.Type == "userId").Value;
                return tokenResult;
            }
            catch (Exception ex)
            {
                if (ex is SecurityTokenExpiredException)
                {
                    var jwtSecurityToken = handler.ReadJwtToken(token);
                    var userId = jwtSecurityToken.Claims.First(x => x.Type == "userId").Value;

                    TokenData onlineTokenData = new()
                    {
                        UserId = userId,
                    };

                    var responseToken = GenerateToken(onlineTokenData, "tallerApiKey");
                    string refreshToken = new JwtSecurityTokenHandler().WriteToken(responseToken);
                    tokenResult.RefreshToken = refreshToken;
                    tokenResult.UserId = userId;
                    tokenResult.Token = refreshToken;
                    return tokenResult;
                }
                tokenResult.HasError = true;
                tokenResult.InnerException = ex.Message;
                return tokenResult;
            }
        }
    }

}

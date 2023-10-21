using Microsoft.IdentityModel.Tokens;
using ToDoListAPI.Models.Authentication;

namespace ToDoListAPI.Services.Interface
{
    public interface IJwtService
    {
        SecurityToken GenerateToken(TokenData tokenData, string secretKeyId);
        TokenData ValidateToken(string token, string secretKeyId);
    }
}

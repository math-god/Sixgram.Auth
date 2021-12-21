using Sixgram.Auth.Core.Dto.User;

namespace Sixgram.Auth.Core.Token
{
    public interface ITokenService
    {
        TokenModel CreateToken(UserModelDto user);
    }
}
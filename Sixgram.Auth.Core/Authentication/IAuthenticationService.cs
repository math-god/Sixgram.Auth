using System.Threading.Tasks;
using Sixgram.Auth.Common.Result;
using Sixgram.Auth.Core.Dto.Authentication;
using Sixgram.Auth.Core.Dto.Authentication.Login;
using Sixgram.Auth.Core.Dto.Authentication.Register;
using Sixgram.Auth.Core.Dto.User.Update;

namespace Sixgram.Auth.Core.Authentication
{
    public interface IAuthenticationService
    {
        Task<ResultContainer<UserLoginResponseDto>> Login(UserLoginRequestDto data);
        Task<ResultContainer<UserRegisterResponseDto>> Register(UserRegisterRequestDto data);
        Task UpdatePassword(UserPasswordUpdateDto data);
    }
}
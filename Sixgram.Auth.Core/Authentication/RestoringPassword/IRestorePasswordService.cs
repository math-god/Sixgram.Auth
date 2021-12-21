using System.Threading.Tasks;
using Sixgram.Auth.Common.Result;
using Sixgram.Auth.Core.Dto.Authentication.RestoringPassword.ForgotPassword;
using Sixgram.Auth.Core.Dto.Authentication.RestoringPassword.RestorePassword;

namespace Sixgram.Auth.Core.Authentication.RestoringPassword
{
    public interface IRestorePasswordService
    {
        Task<ResultContainer<ForgotPasswordResponseDto>> ForgotPassword(ForgotPasswordRequestDto email);
        Task<ResultContainer<RestorePasswordResponseDto>> RestorePassword(RestorePasswordRequestDto data);
    }
}
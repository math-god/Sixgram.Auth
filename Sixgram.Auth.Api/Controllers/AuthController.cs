using System.Threading.Tasks;
using Sixgram.Auth.Common.Result;
using Sixgram.Auth.Core.Authentication;
using Sixgram.Auth.Core.Dto.Authentication.Login;
using Sixgram.Auth.Core.Dto.Authentication.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sixgram.Auth.Core.Authentication.RestoringPassword;
using Sixgram.Auth.Core.Dto.Authentication.RestoringPassword.ForgotPassword;
using Sixgram.Auth.Core.Dto.Authentication.RestoringPassword.RestorePassword;

namespace Sixgram.Auth.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [AllowAnonymous]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authService;
        private readonly IRestorePasswordService _restorePasswordService;
        
        public AuthController
        (
            IAuthenticationService authenticateService,
            IRestorePasswordService restorePasswordService
        )
        {
            _authService = authenticateService;
            _restorePasswordService = restorePasswordService;
        }
        
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Return user Id and token</response>
        /// <response code="400">If the user already exists or Email is not valid</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserRegisterResponseDto>> Register(UserRegisterRequestDto user)
            => await ReturnResult<ResultContainer<UserRegisterResponseDto>, UserRegisterResponseDto>
                (_authService.Register(user));
        

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Return user Id and token</response>
        /// <response code="404">If the user doesn't exist</response>
        /// <response code="400">If the password is not right</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginRequestDto user)
            => await ReturnResult<ResultContainer<UserLoginResponseDto>, UserLoginResponseDto>
                (_authService.Login(user));
        
        /// <summary>
        /// Forgot password
        /// </summary>
        /// <param name="email"></param>
        /// <response code="200">Return Email</response>
        /// <response code="404">If the user with Email doesn't exist</response>
        /// <response code="400">If Email is not valid</response>
        [HttpPost("Forgot-Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ForgotPasswordResponseDto>> ForgotPassword(ForgotPasswordRequestDto email)
            => await ReturnResult<ResultContainer<ForgotPasswordResponseDto>, ForgotPasswordResponseDto>
                (_restorePasswordService.ForgotPassword(email));

        /// <summary>
        /// Restore password
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Return Email</response>
        /// <response code="404">If the user with Email doesn't exist</response>
        /// <response code="400">If new password and confirm password are different or code is not valid</response>
        [HttpPost("Restore-Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RestorePasswordResponseDto>> RestorePassword(RestorePasswordRequestDto data)
            => await ReturnResult<ResultContainer<RestorePasswordResponseDto>, RestorePasswordResponseDto>
                (_restorePasswordService.RestorePassword(data));
    }
}
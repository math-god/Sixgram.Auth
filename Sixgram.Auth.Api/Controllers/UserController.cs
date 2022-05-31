using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sixgram.Auth.Common.Result;
using Sixgram.Auth.Core.Dto.User;
using Sixgram.Auth.Core.Dto.User.Update;
using Sixgram.Auth.Core.User;

namespace Sixgram.Auth.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController
        (
            IUserService userService
        )
        {
            _userService = userService;
        }

        /// <summary>
        /// Change avatar
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Return avatar id</response>
        /// <response code="400">There is no file in request or file storage returns response without file id</response>
        [HttpPatch("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserChangeAvatarResponseDto>> ChangeAvatar(
            [FromForm] UserChangeAvatarRequestDto data)
            => await ReturnResult<ResultContainer<UserChangeAvatarResponseDto>, UserChangeAvatarResponseDto>
                (_userService.ChangeAvatar(data));

        /// <summary>
        /// Edit user information
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Return user Id and token</response>
        /// <response code="400">If new UserName is already exists or age is not valid</response>
        /// <response code="404">If user with UserName doesn't exist</response>
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserUpdateResponseDto>> Edit(UserUpdateRequestDto user)
            => await ReturnResult<ResultContainer<UserUpdateResponseDto>, UserUpdateResponseDto>
                (_userService.Edit(user));

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return user</response>
        /// <response code="404">If user with Id doesn't exist</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModelResponseDto>> GetById(Guid id)
            => await ReturnResult<ResultContainer<UserModelResponseDto>, UserModelResponseDto>
                (_userService.GetById(id));

        /// <summary>
        /// Get user by UserName
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Return user</response>
        /// <response code="404">If user with UserName doesn't exist</response>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModelResponseDto>> GetByUserName(string name)
            => await ReturnResult<ResultContainer<UserModelResponseDto>, UserModelResponseDto>
                (_userService.GetByUserName(name));

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return user</response>
        /// <response code="404">If user doesn't exist</response>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UserModelResponseDto>> Delete(Guid id)
            => await ReturnResult<ResultContainer<UserModelResponseDto>, UserModelResponseDto>
                (_userService.Delete(id));

        /// <summary>
        /// Get user by Jwt
        /// </summary>
        /// <param name="token"></param>
        /// <response code="200">Return user</response>
        /// <response code="404">If user doesn't exist</response>
        [HttpGet("{token:required}")]
        public async Task<ActionResult<UserModelDto>> GetByToken(string token)
            => await ReturnResult<ResultContainer<UserModelDto>, UserModelDto>
                (_userService.GetByToken(token));
    }
}
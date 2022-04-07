using System;
using System.Threading.Tasks;
using Sixgram.Auth.Common.Result;
using Sixgram.Auth.Core.Dto.File;
using Sixgram.Auth.Core.Dto.User;
using Sixgram.Auth.Core.Dto.User.Update;

namespace Sixgram.Auth.Core.User
{
    public interface IUserService
    {
        Task<ResultContainer<UserChangeAvatarResponseDto>> ChangeAvatar(UserChangeAvatarRequestDto data);
        Task<ResultContainer<UserUpdateResponseDto>> Edit(UserUpdateRequestDto data);
        Task<ResultContainer<UserModelResponseDto>> GetByUserName(string userName);
        Task<ResultContainer<UserModelResponseDto>> GetById(Guid id);
        Task<ResultContainer<UserModelResponseDto>> Delete(Guid id);
        Task<ResultContainer<UserListDto>> GetUsersIdByUserName(string userName);
    }
}
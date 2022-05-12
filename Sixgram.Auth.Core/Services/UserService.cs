using System;
using System.Threading.Tasks;
using AutoMapper;
using Sixgram.Auth.Common.Error;
using Sixgram.Auth.Common.Result;
using Sixgram.Auth.Core.Dto.User;
using Sixgram.Auth.Core.Dto.User.Update;
using Sixgram.Auth.Core.File;
using Sixgram.Auth.Core.User;
using Sixgram.Auth.Database.Repository.User;

namespace Sixgram.Auth.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IFileService _fileService;

        public UserService
        (
            IUserRepository userRepository,
            IMapper mapper,
            IUserIdentityService userIdentityService,
            IFileService fileService
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userIdentityService = userIdentityService;
            _fileService = fileService;
        }


        public async Task<ResultContainer<UserChangeAvatarResponseDto>> ChangeAvatar(
            UserChangeAvatarRequestDto data)
        {
            var result = new ResultContainer<UserChangeAvatarResponseDto>();

            if (data.File == null)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var userId = _userIdentityService.GetCurrentUserId();

            var avatarId = await _fileService.Send(data.File, userId);

            if (avatarId == null)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var user = await _userRepository.GetById(userId);

            user.AvatarId = avatarId;

            result = _mapper.Map<ResultContainer<UserChangeAvatarResponseDto>>(await _userRepository.Update(user));

            return result;
        }

        public async Task<ResultContainer<UserUpdateResponseDto>> Edit(UserUpdateRequestDto data)
        {
            var result = new ResultContainer<UserUpdateResponseDto>();
            var user = _userRepository.GetOne(u => u.UserName == data.UserName);
            if (user == null)
                result.ErrorType = ErrorType.NotFound;
            return result;
        }

        public async Task<ResultContainer<UserModelResponseDto>> GetByUserName(string name)
        {
            var result = new ResultContainer<UserModelResponseDto>();
            var user = _userRepository.GetOne(u => u.UserName == name);
            if (user == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<UserModelResponseDto>>(user);
            return result;
        }

        public async Task<ResultContainer<UserModelResponseDto>> GetById(Guid id)
        {
            var result = new ResultContainer<UserModelResponseDto>();
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<UserModelResponseDto>>(user);
            return result;
        }

        public async Task<ResultContainer<UserModelResponseDto>> Delete(Guid id)
        {
            var result = new ResultContainer<UserModelResponseDto>();
            var user = await _userRepository.Delete(id);
            if (user == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<UserModelResponseDto>>(user);
            return result;
        }
    }
}
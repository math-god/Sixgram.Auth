using System;
using System.Threading.Tasks;
using AutoMapper;
using Sixgram.Auth.Common.Error;
using Sixgram.Auth.Common.Result;
using Sixgram.Auth.Core.Dto.User;
using Sixgram.Auth.Core.Dto.User.Update;
using Sixgram.Auth.Core.User;
using Sixgram.Auth.Database.Repository.User;

namespace Sixgram.Auth.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService
        (
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
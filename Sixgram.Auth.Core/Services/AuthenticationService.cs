using System;
using System.Threading.Tasks;
using AutoMapper;
using Sixgram.Auth.Common.Error;
using Sixgram.Auth.Common.Result;
using Sixgram.Auth.Common.Roles;
using Sixgram.Auth.Core.Authentication;
using Sixgram.Auth.Core.Dto.Authentication;
using Sixgram.Auth.Core.Dto.Authentication.Login;
using Sixgram.Auth.Core.Dto.Authentication.Register;
using Sixgram.Auth.Core.Dto.Token;
using Sixgram.Auth.Core.Dto.User;
using Sixgram.Auth.Core.Hashing;
using Sixgram.Auth.Core.Token;
using Sixgram.Auth.Core.Validating;
using Sixgram.Auth.Database.Models;
using Sixgram.Auth.Database.Repository.User;

namespace Sixgram.Auth.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService
        (
            ITokenService tokenService,
            IMapper mapper,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResultContainer<UserLoginResponseDto>> Login(UserLoginRequestDto data)
        {
            var result = new ResultContainer<UserLoginResponseDto>();

            var user = _userRepository.GetOne(
                u => u.Email == data.EmailOrUserName || u.UserName == data.EmailOrUserName);
            if (user == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            var isPasswordValid = _passwordHasher.PasswordMatches(data.Password, user.Password);
            if (!isPasswordValid)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var userDto = _mapper.Map<UserModelDto>(user);

            result = _mapper.Map<ResultContainer<UserLoginResponseDto>>(user);
            result.Data.Token = CreateToken(userDto);

            return result;
        }

        public async Task<ResultContainer<UserRegisterResponseDto>> Register(UserRegisterRequestDto data)
        {
            var result = new ResultContainer<UserRegisterResponseDto>();
            var isEmailValid = EmailValidator.IsEmailValid(data.Email);
            if (!isEmailValid || data.Firstname == null || data.Name == null || data.Age is <= 0 or > 120)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var user = _userRepository.GetOne(u => u.Email == data.Email || u.UserName == data.UserName);
            if (user != null)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }
            
            user = _mapper.Map<UserModel>(data);
            user.Id = new Guid();
            user.Password = _passwordHasher.HashPassword(data.Password);
            user.DateCreated = DateTime.Now;
            user.Role = UserRoles.User;

            result = _mapper.Map<ResultContainer<UserRegisterResponseDto>>(await _userRepository.Create(user));
            
            var userDto = _mapper.Map<UserModelDto>(user);
            
            result.Data.Token = CreateToken(userDto);

            return result;
        }

        private TokenModelDto CreateToken(UserModelDto data)
        {
            var result = _mapper.Map<TokenModelDto>(_tokenService.CreateToken(data));
            return result;
        }

        public async Task UpdatePassword(UserPasswordUpdateDto data)
        {
            var user = _userRepository.GetOne(u => u.Email == data.Email);
            user.Password = _passwordHasher.HashPassword(data.NewPassword);
            user.DateUpdated = DateTime.Now;
            await _userRepository.Update(user);
        }
    }
}
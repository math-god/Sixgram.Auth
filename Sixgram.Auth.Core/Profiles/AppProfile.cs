using AutoMapper;
using Sixgram.Auth.Common.Result;
using Sixgram.Auth.Core.Dto.Authentication.Login;
using Sixgram.Auth.Core.Dto.Authentication.Register;
using Sixgram.Auth.Core.Dto.Authentication.RestoringPassword.ForgotPassword;
using Sixgram.Auth.Core.Dto.Authentication.RestoringPassword.RestorePassword;
using Sixgram.Auth.Core.Dto.User;
using Sixgram.Auth.Core.Dto.User.Update;
using Sixgram.Auth.Database.Models;

namespace Sixgram.Auth.Core.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<UserModel, UserModelDto>();
            CreateMap<UserModel, UserLoginResponseDto>();
            CreateMap<UserModel, UserRegisterResponseDto>();
            CreateMap<UserModel, UserModelResponseDto>();
            CreateMap<UserModel, UserUpdateResponseDto>();
            CreateMap<UserModel, ResultContainer<UserLoginResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u));
            CreateMap<UserModel, ResultContainer<UserRegisterResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u));
            CreateMap<UserModel, ResultContainer<UserModelResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u));
            CreateMap<UserModel, ResultContainer<UserUpdateResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u));
            
            CreateMap<UserModel, ResultContainer<UserChangeAvatarResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u));
            
            CreateMap<UserModel, ResultContainer<UserModelDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(u => u));
            
            CreateMap<UserModel, UserChangeAvatarResponseDto>();

            /*CreateMap<TokenModel, TokenModelDto>();*/

            CreateMap<UserRegisterRequestDto, UserModel>();
            CreateMap<UserRegisterRequestDto, UserModelDto>();

            CreateMap<ForgotPasswordRequestDto, ForgotPasswordResponseDto>();
            CreateMap<ForgotPasswordRequestDto, ResultContainer<ForgotPasswordResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(f => f));
            
            CreateMap<RestorePasswordRequestDto, RestorePasswordResponseDto>();
            CreateMap<RestorePasswordRequestDto, ResultContainer<RestorePasswordResponseDto>>()
                .ForMember("Data", opt =>
                    opt.MapFrom(r => r));
            
        }
    }
}
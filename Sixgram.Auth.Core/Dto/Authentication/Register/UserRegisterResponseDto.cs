using System;
using Sixgram.Auth.Core.Dto.Token;

namespace Sixgram.Auth.Core.Dto.Authentication.Register
{
    public class UserRegisterResponseDto
    {
        public TokenModelDto Token { get; set; }
    }
}
using System;
using Sixgram.Auth.Core.Dto.Token;

namespace Sixgram.Auth.Core.Dto.Authentication.Login
{
    public class UserLoginResponseDto
    {
        public Guid Id { get; set; }
        public TokenModelDto Token { get; set; }
    }
}
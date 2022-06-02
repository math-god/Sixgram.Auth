using System;
using Sixgram.Auth.Common.Base;
using Sixgram.Auth.Common.Roles;

namespace Sixgram.Auth.Core.Dto.User
{
    public class UserModelDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Guid? AvatarId { get; set; }
    }
}
using System;
using Sixgram.Auth.Common.Base;
using Sixgram.Auth.Common.Roles;

namespace Sixgram.Auth.Database.Models
{
    public class UserModel : BaseModel
    {
        public int Age { get; set; }
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public Guid? AvatarId { get; set; }

        public UserRoles Role { get; set; }
    }
}
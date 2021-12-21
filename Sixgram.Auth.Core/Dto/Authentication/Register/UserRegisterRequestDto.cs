using System.ComponentModel.DataAnnotations;

namespace Sixgram.Auth.Core.Dto.Authentication.Register
{
    public class UserRegisterRequestDto
    {
        public string Firstname { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
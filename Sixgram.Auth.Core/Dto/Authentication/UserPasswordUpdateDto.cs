namespace Sixgram.Auth.Core.Dto.Authentication
{
    public class UserPasswordUpdateDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
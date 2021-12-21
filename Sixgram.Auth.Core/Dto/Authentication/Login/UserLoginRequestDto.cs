namespace Sixgram.Auth.Core.Dto.Authentication.Login
{
    public class UserLoginRequestDto
    {
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
    }
}
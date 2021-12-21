namespace Sixgram.Auth.Core.Dto.Authentication.RestoringPassword.RestorePassword
{
    public class RestorePasswordRequestDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}
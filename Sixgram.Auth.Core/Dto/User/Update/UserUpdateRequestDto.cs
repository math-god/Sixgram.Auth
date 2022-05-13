namespace Sixgram.Auth.Core.Dto.User.Update
{
    public class UserUpdateRequestDto
    {
        public string UserName { get; set; }
        public string NewUserName { get; set; }
        public int Age { get; set; }
    }
}
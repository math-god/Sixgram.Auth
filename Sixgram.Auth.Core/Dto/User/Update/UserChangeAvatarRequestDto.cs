using Microsoft.AspNetCore.Http;

namespace Sixgram.Auth.Core.Dto.User.Update;

public class UserChangeAvatarRequestDto
{
    public IFormFile File { get; set; }
}
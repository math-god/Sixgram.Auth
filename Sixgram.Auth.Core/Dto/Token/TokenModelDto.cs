namespace Sixgram.Auth.Core.Dto.Token
{
    public class TokenModelDto
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
    }
}
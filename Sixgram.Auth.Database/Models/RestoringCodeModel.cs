using Sixgram.Auth.Common.Base;

namespace Sixgram.Auth.Database.Models
{
    public class RestoringCodeModel : BaseModel
    {
        public string Code { get; set; }
        public long Expiration { get; set; }
        public string Email { get; set; }
        public bool IsUsed { get; set; }
    }
}
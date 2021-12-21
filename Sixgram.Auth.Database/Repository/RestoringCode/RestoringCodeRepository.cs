using Sixgram.Auth.Database.Models;
using Sixgram.Auth.Database.Repository.Base;

namespace Sixgram.Auth.Database.Repository.RestoringCode
{
    public class RestoringCodeRepository : BaseRepository<RestoringCodeModel>, IRestoringCodeRepository
    {
        public RestoringCodeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
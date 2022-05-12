using System;
using System.Threading.Tasks;
using Sixgram.Auth.Database.Models;
using Sixgram.Auth.Database.Repository.Base;

namespace Sixgram.Auth.Database.Repository.User
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
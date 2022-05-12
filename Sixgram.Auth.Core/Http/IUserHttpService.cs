using System;
using System.Threading.Tasks;

namespace Sixgram.Auth.Core.Http;

public interface IUserHttpService
{
    Task<bool?> DoesUserExist(Guid userId);
}
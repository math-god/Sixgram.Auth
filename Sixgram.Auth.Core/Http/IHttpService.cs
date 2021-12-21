using System.Threading.Tasks;

namespace Sixgram.Auth.Core.Http
{
    public interface IHttpService
    {
        public Task CreateSubscriptionEntity(string json);
    }
}
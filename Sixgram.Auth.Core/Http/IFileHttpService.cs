using System.Net.Http;
using System.Threading.Tasks;
using Sixgram.Auth.Core.Dto.File;

namespace Sixgram.Auth.Core.Http
{
    public interface IFileHttpService
    {
        public Task<string> SendRequest(FileSendingDto fileSendingDto);
    }
}
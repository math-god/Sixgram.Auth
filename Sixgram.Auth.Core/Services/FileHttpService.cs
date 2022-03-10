using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Sixgram.Auth.Core.Dto.File;
using Sixgram.Auth.Core.Http;

namespace Sixgram.Auth.Core.Services
{
    public class FileHttpService : IFileHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;

        public FileHttpService
        (
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpClient = httpClientFactory.CreateClient("file_storage");
            _httpContext = httpContextAccessor.HttpContext;
        }
        

        public async Task<string> SendRequest(FileSendingDto fileSendingDto)
        {
            var bytes = new ByteArrayContent(fileSendingDto.UploadedFile);
            var postId = new StringContent(fileSendingDto.SourceId.ToString());
            var fileSource = new StringContent(fileSendingDto.FileSource.ToString());

            var multiContent = new MultipartFormDataContent();

            multiContent.Add(bytes, "UploadedFile", fileSendingDto.UploadedFileName);
            multiContent.Add(postId, "SourceId");
            multiContent.Add(fileSource, "FileSource");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _httpContext.GetTokenAsync("access_token"));

            var responseMessage = await _httpClient.PostAsync("/api/v1/task/uploadfile", multiContent);
            
            if  (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }

            return null;
        }
    }
}
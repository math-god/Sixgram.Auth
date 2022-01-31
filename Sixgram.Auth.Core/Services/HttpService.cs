using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using Sixgram.Auth.Core.Http;

namespace Sixgram.Auth.Core.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(IHttpClientFactory httpClientFactory)
        {

            _httpClient = httpClientFactory.CreateClient("posts");
            
        }

        public async Task CreateMember(string token)
        {
            var httpContent = new StringContent("");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using var httpResponse =
                await _httpClient.PostAsync("/api/Membership/CreateMember", httpContent);
            
            Console.WriteLine(httpResponse.StatusCode);
        }
    }
}
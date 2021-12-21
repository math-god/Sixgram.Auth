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

        public async Task CreateSubscriptionEntity(string token)
        {
            var httpContent = new StringContent(token, Encoding.UTF32, "application/jwt");

            using var httpResponse =
                await _httpClient.PostAsync("/api/Subscribe/CreateSubscriptionEntity", httpContent);
            
            
            Console.WriteLine(httpResponse.StatusCode);
        }
    }
}
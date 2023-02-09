using eStore.Models;
using eStore.Models.Dto;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace eStore.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string authUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            authUrl = configuration.GetValue<string>("APIUrls:MemberAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.POST,
                Data = obj,
                Url = authUrl + "Login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.POST,
                Data = obj,
                Url = authUrl + "Register"
            });
        }
    }
}

using eStore.Models;
using eStore.Models.Dto;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace eStore.Services
{
    public class MemberService : BaseService, IMemberService
    {
        private string memberUrl;

        public MemberService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            memberUrl = configuration.GetValue<string>("APIUrls:MemberAPI");
        }

        public Task<T> CreateAsync<T>(RegisterationRequestDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.POST,
                Data = dto,
                Url = memberUrl
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.DELETE,
                Url = memberUrl + id,
            });
        }

        public Task<T> GetAllAsync<T>(string? search)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = memberUrl + "?search=" + search,
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = memberUrl + id,
            });
        }

        public Task<T> UpdateAsync<T>(MemberEditDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.PUT,
                Data = dto,
                Url = memberUrl + dto.MemberId,
            });
        }
    }
}

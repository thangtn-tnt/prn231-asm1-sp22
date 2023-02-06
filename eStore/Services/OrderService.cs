using eStore.Models;
using eStore.Models.Dto;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace eStore.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private string orderUrl;

        public OrderService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            orderUrl = configuration.GetValue<string>("APIUrls:OrderAPI");
        }

        public Task<T> CreateAsync<T>(OrderCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.POST,
                Data = dto,
                Url = orderUrl
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.DELETE,
                Url = orderUrl + id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = orderUrl,
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = orderUrl + id,
            });
        }

        public Task<T> UpdateAsync<T>(ProductUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.PUT,
                Data = dto,
                Url = orderUrl + dto.ProductId,
            });
        }
    }
}

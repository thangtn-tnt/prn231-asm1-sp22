using eStore.Models;
using eStore.Models.Dto;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace eStore.Services
{
    public class OrderDetailService : BaseService, IOrderDetailService
    {
        private string orderDetailUrl;

        public OrderDetailService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            orderDetailUrl = configuration.GetValue<string>("APIUrls:OrderDetailAPI");
        }

        public Task<T> CreateAsync<T>(ProductSalesDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.POST,
                Data = dto,
                Url = orderDetailUrl
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.DELETE,
                Url = orderDetailUrl + id,
            });
        }

        public Task<T> GetAllAsync<T>(string? productName)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = string.IsNullOrEmpty(productName) ? orderDetailUrl : orderDetailUrl + "?search=" + productName,
            });
        }

        public Task<T> GetAsync<T>(int memberId)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = orderDetailUrl + memberId,
            });
        }

        public Task<T> UpdateAsync<T>(ProductSalesDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.PUT,
                Data = dto,
                Url = orderDetailUrl + dto.OrderId,
            });
        }
    }
}

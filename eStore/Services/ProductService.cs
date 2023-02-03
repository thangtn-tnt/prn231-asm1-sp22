using eStore.Models;
using eStore.Models.Dto;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace eStore.Services
{
    public class ProductService : BaseService, IProductService
    {        
        private string productUrl;

        public ProductService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {            
            productUrl = configuration.GetValue<string>("APIUrls:ProductAPI");
        }

        public Task<T> CreateAsync<T>(ProductCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.POST,
                Data = dto,
                Url = productUrl,
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.DELETE,
                Url = productUrl + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = productUrl,                
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                Url = productUrl + id,                
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ProductDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.PUT,
                Data = dto,
                Url = productUrl + dto.ProductId,
                Token = token
            });
        }
    }
}

using eStore.Models;
using eStore.Models.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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

        public Task<T> CreateAsync<T>(ProductCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.POST,
                Data = dto,
                Url = productUrl                
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

        public Task<T> UpdateAsync<T>(ProductDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.PUT,
                Data = dto,
                Url = productUrl + dto.ProductId,                
            });
        }

        public Task<T> GetForeignKeyList<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,                
                Url = productUrl + "Categories"
            });
        }
    }
}

using eStore.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eStore.Services
{
    public interface IProductService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ProductCreateDTO dto);
        Task<T> UpdateAsync<T>(ProductDTO dto);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> GetForeignKeyList<T>();
    }
}

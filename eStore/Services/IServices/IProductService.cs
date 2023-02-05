using eStore.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eStore.Services
{
    public interface IProductService
    {
        Task<T> GetAllAsync<T>(string? productName);
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ProductCreateDTO dto);
        Task<T> UpdateAsync<T>(ProductUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> GetForeignKeyList<T>();
    }
}

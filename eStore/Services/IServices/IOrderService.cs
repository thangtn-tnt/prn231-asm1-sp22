using eStore.Models.Dto;
using System.Threading.Tasks;

namespace eStore.Services
{
    public interface IOrderService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(OrderCreateDTO dto);
        Task<T> UpdateAsync<T>(ProductUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}

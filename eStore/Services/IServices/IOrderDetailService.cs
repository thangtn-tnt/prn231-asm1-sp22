using eStore.Models.Dto;
using System.Threading.Tasks;

namespace eStore.Services
{
    public interface IOrderDetailService
    {
        Task<T> GetAllAsync<T>(string? productName);
        Task<T> GetAsync<T>(int memberId);
        Task<T> CreateAsync<T>(ProductSalesDTO dto);
        Task<T> UpdateAsync<T>(ProductSalesDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}

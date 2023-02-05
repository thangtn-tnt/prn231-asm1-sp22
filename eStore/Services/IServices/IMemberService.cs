using eStore.Models.Dto;
using System.Threading.Tasks;

namespace eStore.Services
{
    public interface IMemberService
    {
        Task<T> GetAllAsync<T>(string? search);
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(RegisterationRequestDTO dto);
        Task<T> UpdateAsync<T>(MemberEditDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}

using eStore.Models.Dto;
using System.Threading.Tasks;

namespace eStore.Services
{
    public interface IMemberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(RegisterationRequestDTO dto);
        Task<T> UpdateAsync<T>(MemberDTO dto);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}

using eStore.Models;
using System.Threading.Tasks;

namespace eStore.Services
{
    public interface IBaseService
    {
        APIResponse APIResponse { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}

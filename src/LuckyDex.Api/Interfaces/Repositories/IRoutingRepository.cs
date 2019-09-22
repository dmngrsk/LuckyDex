using LuckyDex.Api.Models;
using System.Threading.Tasks;

namespace LuckyDex.Api.Interfaces.Repositories
{
    public interface IRoutingRepository
    {
        Task<Routing> GetAsync(string name);
    }
}

using LuckyDex.Api.Models;
using System.Threading.Tasks;

namespace LuckyDex.Api.Interfaces.Repositories
{
    public interface ITrainerRepository
    {
        Task<Trainer> GetAsync(string name);
        Task PutAsync(Trainer trainer);
    }
}

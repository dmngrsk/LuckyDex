using LuckyDex.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuckyDex.Api.Interfaces.Repositories
{
    public interface ITrainerRepository
    {
        Task<IReadOnlyCollection<Trainer>> GetAllAsync();
        Task<Trainer> GetAsync(string name);
        Task PutAsync(Trainer trainer);
    }
}

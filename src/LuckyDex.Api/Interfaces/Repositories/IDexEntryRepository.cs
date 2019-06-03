using LuckyDex.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuckyDex.Api.Interfaces.Repositories
{
    public interface IDexEntryRepository
    {
        Task<IReadOnlyCollection<DexEntry>> GetTrainerEntriesAsync(string name);
        Task<IReadOnlyCollection<DexEntry>> GetPokémonEntriesAsync(string id);
        Task PutTrainerEntriesAsync(IReadOnlyCollection<DexEntry> entries, bool removeOthers);
    }
}

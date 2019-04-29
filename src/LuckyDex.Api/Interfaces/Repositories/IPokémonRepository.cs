using LuckyDex.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuckyDex.Api.Interfaces.Repositories
{
    public interface IPokémonRepository
    {
        Task<IReadOnlyCollection<Pokémon>> GetManyAsync(bool? isTradeable = null, bool? isLowestForm = null, bool? isLegendary = null);
        Task<Pokémon> GetAsync(int id);
        Task PutAsync(int id, Pokémon pokémon);
    }
}

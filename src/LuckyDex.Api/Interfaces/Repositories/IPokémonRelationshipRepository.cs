using LuckyDex.Api.Models;
using System.Threading.Tasks;

namespace LuckyDex.Api.Interfaces.Repositories
{
    public interface IPokémonRelationshipRepository
    {
        Task<PokémonRelationship> GetAsync(string id);
    }
}

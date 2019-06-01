using LuckyDex.Api.Models;
using System.Threading.Tasks;

namespace LuckyDex.Api.Interfaces.Repositories
{
    public interface ITrainerRelationshipRepository
    {
        Task<TrainerRelationship> GetAsync(string name);
        Task PutAsync(TrainerRelationship relationship);
    }
}

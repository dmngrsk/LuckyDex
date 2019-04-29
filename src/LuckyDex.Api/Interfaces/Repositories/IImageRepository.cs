using LuckyDex.Api.Models;
using System.Threading.Tasks;

namespace LuckyDex.Api.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<Image> GetAsync(int id);
        Task PutAsync(int id, Image image);
    }
}

using Microsoft.Azure.Cosmos.Table;

namespace LuckyDex.Api.Models.TableStorage
{
    public class PokémonEntity : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTradeable { get; set; }
        public bool IsLowestForm { get; set; }
        public bool IsLegendary { get; set; }
    }
}

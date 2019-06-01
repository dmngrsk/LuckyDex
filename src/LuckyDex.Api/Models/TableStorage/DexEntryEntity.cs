using Microsoft.Azure.Cosmos.Table;

namespace LuckyDex.Api.Models.TableStorage
{
    public class DexEntryEntity : TableEntity
    {
        public string PokémonId { get; set; }
        public string PokémonName { get; set; }
        public string TrainerName { get; set; }

        public DexEntryEntity()
        {
        }

        public DexEntryEntity(string pokémonId, string pokémonName, string trainerName)
        {
            PokémonId = pokémonId;
            PokémonName = pokémonName;
            TrainerName = trainerName;

            RowKey = $"{trainerName}_{pokémonId}";
            PartitionKey = "x";
            ETag = "*";
        }
    }
}

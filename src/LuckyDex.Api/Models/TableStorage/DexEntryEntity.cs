using Microsoft.Azure.Cosmos.Table;

namespace LuckyDex.Api.Models.TableStorage
{
    public class DexEntryEntity : TableEntity
    {
        public string PokémonId { get; set; }
        public string TrainerName { get; set; }

        public DexEntryEntity()
        {
        }

        public DexEntryEntity(string pokémonId, string trainerName)
        {
            PokémonId = pokémonId;
            TrainerName = trainerName;

            RowKey = $"{trainerName}_{pokémonId}";
            PartitionKey = "x";
            ETag = "*";
        }
    }
}

using LuckyDex.Api.Models;
using LuckyDex.Api.Models.TableStorage;

namespace LuckyDex.Api.Mappers
{
    public class PokémonEntityMapper
    {
        public PokémonEntity ToEntity(Pokémon pokémon)
        {
            if (pokémon == null) return null;

            return new PokémonEntity
            {
                PartitionKey = "Pokémon",
                RowKey = pokémon.Id.ToString(),
                Id = pokémon.Id,
                Name = pokémon.Name,
                IsTradeable = pokémon.IsTradeable,
                IsLowestForm = pokémon.IsLowestForm,
                IsLegendary = pokémon.IsLegendary
            };
        }

        public Pokémon FromEntity(PokémonEntity entity)
        {
            if (entity == null) return null;

            return new Pokémon
            {
                Id = entity.Id,
                Name = entity.Name,
                IsTradeable = entity.IsTradeable,
                IsLowestForm = entity.IsLowestForm,
                IsLegendary = entity.IsLegendary
            };
        }
    }
}

using System.Collections.Generic;

namespace LuckyDex.Api.Models
{
    public class PokémonRelationship
    {
        public Pokémon Pokémon { get; set; }
        public IReadOnlyCollection<Trainer> Trainers { get; set; }
    }
}

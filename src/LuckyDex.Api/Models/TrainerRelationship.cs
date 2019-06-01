using System.Collections.Generic;

namespace LuckyDex.Api.Models
{
    public class TrainerRelationship
    {
        public Trainer Trainer { get; set; }
        public IReadOnlyCollection<Pokémon> Pokémon { get; set; }
    }
}

using LuckyDex.Api.Models;
using LuckyDex.Api.Models.TableStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuckyDex.Api.Mappers
{
    public class DexEntryEntityMapper
    {
        public TrainerRelationship ToTrainerRelationship(Trainer trainer, IReadOnlyCollection<DexEntryEntity> entities)
        {
            if (entities.Any(e => e.TrainerName != trainer.Name))
            {
                throw new ArgumentException("The collection should contain dex entries of a single trainer only.");
            }

            return new TrainerRelationship
            {
                Trainer = trainer,
                Pokémon = entities.Select(e => new Pokémon { Id = e.PokémonId }).ToList()
            };
        }

        public PokémonRelationship ToPokémonRelationship(IReadOnlyCollection<DexEntryEntity> entities)
        {
            try
            {
                if (!entities.Any())
                {
                    return null;
                }

                var pokémonId = entities.Select(e => e.PokémonId).Distinct().Single();
                
                return new PokémonRelationship
                {
                    Pokémon = new Pokémon { Id = pokémonId },
                    Trainers = entities.Select(e => new Trainer { Name = e.TrainerName }).ToList()
                };
            }
            catch (Exception e)
            {
                throw new ArgumentException("The collection should contain dex entries of a single Pokémon only.", e);
            }
        }

        public IReadOnlyCollection<DexEntryEntity> FromTrainerRelationship(TrainerRelationship relationship)
        {
            return relationship?.Pokémon.Select(p => new DexEntryEntity(p.Id, relationship.Trainer.Name)).ToList();
        }
    }
}

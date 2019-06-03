using LuckyDex.Api.Extensions;
using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Mappers;
using LuckyDex.Api.Models;
using LuckyDex.Api.Models.AppSettings;
using LuckyDex.Api.Models.TableStorage;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuckyDex.Api.Repositories
{
    public class TableStorageTrainerRelationshipRepository : ITrainerRelationshipRepository
    {
        private readonly Lazy<CloudTable> _table;

        private readonly ITrainerRepository _trainerRepository;
        private readonly DexEntryEntityMapper _mapper;

        public TableStorageTrainerRelationshipRepository(ITrainerRepository trainerRepository, TableStorageSettings settings)
        {
            _table = new Lazy<CloudTable>(() =>
            {
                var account = CloudStorageAccount.Parse(settings.ConnectionString);
                var tableClient = account.CreateCloudTableClient();

                var table = tableClient.GetTableReference(settings.DexEntriesTableName);
                table.CreateIfNotExistsAsync().Wait();

                return table;
            });

            _trainerRepository = trainerRepository;
            _mapper = new DexEntryEntityMapper();
        }

        public async Task<TrainerRelationship> GetAsync(string name)
        {
            var table = _table.Value;

            var filter = TableQuery.GenerateFilterCondition
            (
                nameof(DexEntryEntity.TrainerName),
                QueryComparisons.Equal,
                name
            );

            var query = new TableQuery<DexEntryEntity>().Where(filter);
            var entries = new List<DexEntryEntity>();

            TableContinuationToken token = null;
            do
            {
                var result = await table.ExecuteQuerySegmentedAsync(query, token);

                entries.AddRange(result.Results);
                token = result.ContinuationToken;

            } while (token != null);

            var trainerEntity = await _trainerRepository.GetAsync(name);
            var trainer = new Trainer { Name = name, Comment = trainerEntity?.Comment };

            return _mapper.ToTrainerRelationship(trainer, entries);
        }

        public async Task PutAsync(TrainerRelationship relationship)
        {
            await _trainerRepository.PutAsync(relationship.Trainer);

            var table = _table.Value;

            var newEntityIds = relationship.Pokémon.Select(p => p.Id);

            var existingEntities = _mapper.FromTrainerRelationship(await GetAsync(relationship.Trainer.Name));
            var existingEntityIds = existingEntities?.Select(e => e.PokémonId);

            var entitiesToRemove = existingEntities?.Where(e => !newEntityIds.Contains(e.PokémonId)).ChunkBy(100);

            if (entitiesToRemove != null)
            {
                foreach (var chunk in entitiesToRemove)
                {
                    var batch = new TableBatchOperation();

                    foreach (var e in chunk)
                    {
                        var operation = TableOperation.Delete(e);
                        batch.Add(operation);
                    }

                    await table.ExecuteBatchAsync(batch);
                }
            }

            var entitiesToUpsert = _mapper
                .FromTrainerRelationship(relationship)
                .Where(e => existingEntityIds?.Contains(e.PokémonId) != true);

            foreach (var chunk in entitiesToUpsert.ChunkBy(100))
            {
                var batch = new TableBatchOperation();

                foreach (var e in chunk)
                {
                    var operation = TableOperation.Insert(e);
                    batch.Add(operation);
                }

                await table.ExecuteBatchAsync(batch);
            }
        }
    }
}

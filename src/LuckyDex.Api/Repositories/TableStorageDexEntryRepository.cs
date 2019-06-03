using LuckyDex.Api.Extensions;
using LuckyDex.Api.Interfaces.Repositories;
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
    public class TableStorageDexEntryRepository : IDexEntryRepository
    {
        private readonly Lazy<CloudTable> _table;

        public TableStorageDexEntryRepository(TableStorageSettings settings)
        {
            _table = new Lazy<CloudTable>(() =>
            {
                var account = CloudStorageAccount.Parse(settings.ConnectionString);
                var tableClient = account.CreateCloudTableClient();

                var table = tableClient.GetTableReference(settings.DexEntriesTableName);
                table.CreateIfNotExistsAsync().Wait();

                return table;
            });
        }

        public async Task<IReadOnlyCollection<DexEntry>> GetTrainerEntriesAsync(string name)
        {
            var filter = TableQuery.GenerateFilterCondition
            (
                nameof(DexEntryEntity.TrainerName),
                QueryComparisons.Equal,
                name
            );

            return await GetEntriesAsync(filter);
        }
        
        public async Task<IReadOnlyCollection<DexEntry>> GetPokémonEntriesAsync(string id)
        {
            var filter = TableQuery.GenerateFilterCondition
            (
                nameof(DexEntryEntity.PokémonId),
                QueryComparisons.Equal,
                id
            );

            return await GetEntriesAsync(filter);
        }

        public async Task PutTrainerEntriesAsync(IReadOnlyCollection<DexEntry> entries, bool removeOthers)
        {
            var table = _table.Value;
            var trainerName = entries.First().TrainerName;

            if (entries.Any(e => e.TrainerName != trainerName))
            {
                throw new ArgumentException("All entries must point to the same trainer.");
            }

            var existingEntities = await GetTrainerEntriesAsync(trainerName);

            if (removeOthers)
            {
                var newEntityIds = entries.Select(e => e.PokémonId);
                var entitiesToRemove = existingEntities?.Where(e => !newEntityIds.Contains(e.PokémonId)).ChunkBy(100);

                if (entitiesToRemove != null)
                {
                    foreach (var chunk in entitiesToRemove)
                    {
                        var batch = new TableBatchOperation();

                        foreach (var e in chunk)
                        {
                            var operation = TableOperation.Delete(new DexEntryEntity(e.PokémonId, e.TrainerName));
                            batch.Add(operation);
                        }

                        await table.ExecuteBatchAsync(batch);
                    }
                }
            }

            var entriesToUpsert = entries.Where(e => existingEntities?.Any(ee => ee.PokémonId == e.PokémonId) != true);

            foreach (var chunk in entriesToUpsert.ChunkBy(100))
            {
                var batch = new TableBatchOperation();

                foreach (var e in chunk)
                {
                    var operation = TableOperation.Insert(new DexEntryEntity(e.PokémonId, e.TrainerName));
                    batch.Add(operation);
                }

                await table.ExecuteBatchAsync(batch);
            }
        }

        private async Task<IReadOnlyCollection<DexEntry>> GetEntriesAsync(string filter)
        {
            var table = _table.Value;

            var query = new TableQuery<DexEntryEntity>().Where(filter);
            var entries = new List<DexEntryEntity>();

            TableContinuationToken token = null;
            do
            {
                var result = await table.ExecuteQuerySegmentedAsync(query, token);

                entries.AddRange(result.Results);
                token = result.ContinuationToken;

            } while (token != null);

            return entries.Select(e => new DexEntry { PokémonId = e.PokémonId, TrainerName = e.TrainerName }).ToList();
        }
    }
}

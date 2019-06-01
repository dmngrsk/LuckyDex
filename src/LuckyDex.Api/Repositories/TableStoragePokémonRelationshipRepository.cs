using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Mappers;
using LuckyDex.Api.Models;
using LuckyDex.Api.Models.AppSettings;
using LuckyDex.Api.Models.TableStorage;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuckyDex.Api.Repositories
{
    public class TableStoragePokémonRelationshipRepository : IPokémonRelationshipRepository
    {
        private readonly Lazy<CloudTable> _table;
        private readonly DexEntryEntityMapper _mapper;

        public TableStoragePokémonRelationshipRepository(TableStorageSettings settings)
        {
            _table = new Lazy<CloudTable>(() =>
            {
                var account = CloudStorageAccount.Parse(settings.ConnectionString);
                var tableClient = account.CreateCloudTableClient();

                var table = tableClient.GetTableReference(settings.DexEntriesTableName);
                table.CreateIfNotExistsAsync().Wait();

                return table;
            });

            _mapper = new DexEntryEntityMapper();
        }
        
        public async Task<PokémonRelationship> GetAsync(string id)
        {
            var table = _table.Value;

            var filter = TableQuery.GenerateFilterCondition
            (
                nameof(DexEntryEntity.PokémonId),
                QueryComparisons.Equal,
                id
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

            return _mapper.ToPokémonRelationship(entries);
        }
    }
}

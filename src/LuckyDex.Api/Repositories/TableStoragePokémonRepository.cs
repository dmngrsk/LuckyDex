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
    public class TableStoragePokémonRepository : IPokémonRepository
    {
        private readonly Lazy<CloudTable> _table;
        private readonly PokémonEntityMapper _mapper;

        public TableStoragePokémonRepository(TableStorageSettings settings)
        {
            _table = new Lazy<CloudTable>(() =>
            {
                var account = CloudStorageAccount.Parse(settings.ConnectionString);
                var tableClient = account.CreateCloudTableClient();

                var table = tableClient.GetTableReference(settings.PokemonTableName);
                table.CreateIfNotExistsAsync().Wait();

                return table;
            });

            _mapper = new PokémonEntityMapper();
        }
        
        public async Task<IReadOnlyCollection<Pokémon>> GetManyAsync(bool? isTradeable = null, bool? isLowestForm = null, bool? isLegendary = null)
        {
            var table = _table.Value;

            var filter = MergeConditions
            (
                TableQuery.GenerateFilterCondition(nameof(PokémonEntity.PartitionKey), QueryComparisons.Equal, "Pokémon"),
                isTradeable.HasValue ? TableQuery.GenerateFilterConditionForBool(nameof(PokémonEntity.IsTradeable), QueryComparisons.Equal, isTradeable.Value) : null,
                isLowestForm.HasValue ? TableQuery.GenerateFilterConditionForBool(nameof(PokémonEntity.IsLowestForm), QueryComparisons.Equal, isLowestForm.Value) : null,
                isLegendary.HasValue ? TableQuery.GenerateFilterConditionForBool(nameof(PokémonEntity.IsLegendary), QueryComparisons.Equal, isLegendary.Value) : null
            );

            var query = new TableQuery<PokémonEntity>().Where(filter);

            var pokémon = new List<Pokémon>();

            TableContinuationToken token = null;
            do
            {
                var result = await table.ExecuteQuerySegmentedAsync(query, token);

                pokémon.AddRange(result.Results.Select(_mapper.FromEntity));
                token = result.ContinuationToken;

            } while (token != null);

            return pokémon;
        }
        
        public async Task<Pokémon> GetAsync(int id)
        {
            var table = _table.Value;

            var filter = MergeConditions
            (
                TableQuery.GenerateFilterCondition(nameof(PokémonEntity.PartitionKey), QueryComparisons.Equal, "Pokémon"),
                TableQuery.GenerateFilterCondition(nameof(PokémonEntity.RowKey), QueryComparisons.Equal, id.ToString())
            );

            var query = new TableQuery<PokémonEntity>().Where(filter);

            var result = await table.ExecuteQuerySegmentedAsync(query, null);

            return _mapper.FromEntity(result.Results.SingleOrDefault());
        }

        public async Task PutAsync(int id, Pokémon pokémon)
        {
            var table = _table.Value;

            var operation = TableOperation.InsertOrReplace(_mapper.ToEntity(pokémon));

            await Task.FromResult(table.ExecuteAsync(operation));
        }

        private static string MergeConditions(params string[] conditions)
        {
            return conditions
                .Where(c => c != null)
                .Aggregate(
                    (acc, condition) => 
                    TableQuery.CombineFilters(acc, TableOperators.And, condition));
        }
    }
}

using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using LuckyDex.Api.Models.AppSettings;
using LuckyDex.Api.Models.TableStorage;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LuckyDex.Api.Repositories
{
    public class TableStorageRoutingRepository : IRoutingRepository
    {
        private readonly Lazy<CloudTable> _table;

        public TableStorageRoutingRepository(TableStorageSettings settings)
        {
            _table = new Lazy<CloudTable>(() =>
            {
                var account = CloudStorageAccount.Parse(settings.ConnectionString);
                var tableClient = account.CreateCloudTableClient();

                var table = tableClient.GetTableReference(settings.RoutingTableName);
                table.CreateIfNotExistsAsync().Wait();

                return table;
            });
        }

        public async Task<Routing> GetAsync(string name)
        {
            var table = _table.Value;

            var filter = TableQuery.GenerateFilterCondition
            (
                nameof(RoutingEntity.RowKey),
                QueryComparisons.Equal,
                name
            );

            var query = new TableQuery<RoutingEntity>().Where(filter);

            var result = await table.ExecuteQuerySegmentedAsync(query, null);

            return result.Results.Select(e => new Routing { Name = e.RowKey, Route = e.Route }).FirstOrDefault() ?? Routing.Default(name);
        }

        public async Task PutAsync(Routing routing)
        {
            var table = _table.Value;

            var operation = TableOperation.InsertOrReplace(new RoutingEntity(routing.Name, routing.Route));

            await table.ExecuteAsync(operation);
        }
    }
}

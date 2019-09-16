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
    public class TableStorageTrainerRepository : ITrainerRepository
    {
        private readonly Lazy<CloudTable> _table;

        public TableStorageTrainerRepository(TableStorageSettings settings)
        {
            _table = new Lazy<CloudTable>(() =>
            {
                var account = CloudStorageAccount.Parse(settings.ConnectionString);
                var tableClient = account.CreateCloudTableClient();

                var table = tableClient.GetTableReference(settings.TrainerTableName);
                table.CreateIfNotExistsAsync().Wait();

                return table;
            });
        }

        public async Task<IReadOnlyCollection<Trainer>> GetAllAsync()
        {
            var table = _table.Value;

            var filter = TableQuery.GenerateFilterCondition
            (
                nameof(TrainerEntity.PartitionKey),
                QueryComparisons.Equal,
                "x"
            );
            
            var query = new TableQuery<TrainerEntity>().Where(filter);
            var entries = new List<TrainerEntity>();

            TableContinuationToken token = null;
            do
            {
                var result = await table.ExecuteQuerySegmentedAsync(query, token);

                entries.AddRange(result.Results);
                token = result.ContinuationToken;

            } while (token != null);

            return entries.Select(e => new Trainer { Name = e.RowKey, Comment = e.Comment, LastModified = e.Timestamp.LocalDateTime }).ToList();
        }

        public async Task<Trainer> GetAsync(string name)
        {
            var table = _table.Value;

            var filter = TableQuery.GenerateFilterCondition
            (
                nameof(TrainerEntity.RowKey),
                QueryComparisons.Equal,
                name
            );

            var query = new TableQuery<TrainerEntity>().Where(filter);

            var result = await table.ExecuteQuerySegmentedAsync(query, null);

            return result.Results.Select(e => new Trainer { Name = e.RowKey, Comment = e.Comment, LastModified = e.Timestamp.LocalDateTime }).FirstOrDefault() ?? Trainer.Default(name);
        }

        public async Task PutAsync(Trainer trainer)
        {
            var table = _table.Value;

            var operation = TableOperation.InsertOrReplace(new TrainerEntity(trainer.Name, trainer.Comment));

            await table.ExecuteAsync(operation);
        }
    }
}

using Microsoft.Azure.Cosmos.Table;

namespace LuckyDex.Api.Models.TableStorage
{
    public class RoutingEntity : TableEntity
    {
        public string Name { get; set; }
        public string Route { get; set; }

        public RoutingEntity()
        {
        }

        public RoutingEntity(string name, string route)
        {
            RowKey = name;
            Route = route;

            PartitionKey = "x";
            ETag = "*";
        }
    }
}
